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
using konatus.business.Enums;

namespace konatus.api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MaintenanceController : MainController
    {
        private readonly IMaintenanceService _service;
        private readonly IAuthService _authService;
        private readonly IEmailSender _email;
        private readonly FolderSettings _folderSettings;
        private readonly IMapper _mapper;

        public MaintenanceController(IMaintenanceService service,

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
        [HttpPost("GetByUserId")]
        public async Task<ActionResult<IEnumerable<MaintenanceViewModel>>> GetByUserId(IdViewModel viewModel)
        {
            try
            {
                var result = await _service.GetByUserId(viewModel.Id);

                var model = _mapper.Map<IEnumerable<MaintenanceViewModel>>(result);

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
        public async Task<ActionResult<bool>> Add(MaintenanceViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);

                var contactModel = _mapper.Map<MaintenanceModel>(viewModel);
                contactModel.StatusMaintenance = StatusMaintenance.InExecution;

                await _service.Add(contactModel);

                return CustomResponse(viewModel);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"ERROR {ex.Message}");
            }
        }

        [HttpPost("Update")]
        [AllowAnonymous]
        public async Task<ActionResult<bool>> Update(MaintenanceViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);

                var contactModel = _mapper.Map<MaintenanceModel>(viewModel);
                await _service.Update(contactModel);

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