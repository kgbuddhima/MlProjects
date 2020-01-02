using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Abp.UI;
using DermPOC.Shared.Predict;
using DermPOCAppML.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Uploader;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PredictController : ControllerBase
    {
        private readonly ILogger<PredictController> _logger;
        private readonly ConsumeModel consumeModel;

        public PredictController(ILogger<PredictController> logger)
        {
            _logger = logger;
            consumeModel = new ConsumeModel();
        }

        // POST: api/Predict
        [HttpPost]
        public async Task<Shared.Predict.Result> Post([FromForm(Name = "image")] IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new UserFriendlyException("Please select an image");

            string executableLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var tempFolder = Path.Combine(executableLocation, "temp");

            if (!Directory.Exists(tempFolder))
            {
                _logger.LogInformation($"Creating '{tempFolder}' directory due to non-existence");
                Directory.CreateDirectory(tempFolder);
            }

            var tempFileName = await Files.Upload(tempFolder, file);

            if (tempFileName == null)
            {
                _logger.LogError($"tempFileName came in as null");
                throw new UserFriendlyException("Error uploading image");
            }

            tempFileName = $"{tempFileName}{Path.GetExtension(file.FileName)}";

            var modelInput = new ModelInput();
            modelInput.ImageSource = Path.Combine(tempFolder, tempFileName);
            _logger.LogInformation($"The imageSource is '{modelInput.ImageSource}'");

            var prediction = ConsumeModel.Predict(modelInput);

            _logger.LogInformation($"Deleting temp file {tempFileName}");
            bool deleteResult = Files.Delete(tempFolder, tempFileName);

            if (!deleteResult)
            {
                _logger.LogError($"Error deleting temp file {tempFileName}");
            }

            return prediction;
        }
    }
}
