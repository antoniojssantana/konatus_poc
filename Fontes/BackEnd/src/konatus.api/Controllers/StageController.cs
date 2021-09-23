using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using konatus.api.ViewModels;
using konatus.business.Interfaces.Services;
using konatus.business.Models;
using konatus.business.Notifications;
using konatus.api.Interfaces;
using sga.utils.Interfaces;
using konatus.api.Extensions;
using Microsoft.Extensions.Options;

namespace konatus.api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class StageController : MainController
    {
        private readonly IStageService _service;
        private readonly IAuthService _authService;
        private readonly IEmailSender _email;
        private readonly FolderSettings _folderSettings;
        private readonly IMapper _mapper;

        public StageController(IStageService service,

                                IAuthService authService,
                                IOptions<FolderSettings> folderSettings,
                                IEmailSender email,
                                IMapper mapper,
                                 IHttpContextAccessor contextAccessor,
                                INotifier notifier) : base(notifier, contextAccessor)
        {
            _service = service;
            _authService = authService;
            _folderSettings = folderSettings.Value;
            _mapper = mapper;
            _email = email;
        }

        [AllowAnonymous]
        [HttpPost("GetByMaintenancetId")]
        public async Task<ActionResult<IEnumerable<StageViewModel>>> GetByMaintenancetId(IdViewModel viewModel)
        {
            try
            {
                var result = await _service.GetByMaintenancetId(viewModel.Id);

                var model = _mapper.Map<IEnumerable<StageViewModel>>(result);

                if (model == null) return NotFound();

                return CustomResponse(model);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"ERROR {ex.Message}");
            }
        }

        [HttpPost("ChangeState")]
        public async Task<ActionResult> ChangeState(IdViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);
                await _service.ChangeState(viewModel.Id);
                return CustomResponse();
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"ERROR {ex.Message}");
            }
        }

        [AllowAnonymous]
        [HttpPost("Add")]
        public async Task<ActionResult<bool>> Add(StageViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);

                var model = _mapper.Map<StageModel>(viewModel);

                await _service.Add(model);

                return CustomResponse(viewModel);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"ERROR {ex.Message}");
            }
        }

        [HttpPost("Update")]
        [AllowAnonymous]
        public async Task<ActionResult<bool>> Update(StageViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);

                var model = _mapper.Map<StageModel>(viewModel);
                await _service.Update(model);

                return CustomResponse(viewModel);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"ERROR {ex.Message}");
            }
        }

        [AllowAnonymous]
        [HttpPost("Delete")]
        public async Task<ActionResult<bool>> Delete(IdViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();
                var originSiteModel = await _service.GetId(viewModel.Id);
                if (originSiteModel == null) return NotFound();
                await _service.Delete(originSiteModel);

                return CustomResponse(viewModel);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"ERROR {ex.Message}");
            }
        }
    }
}