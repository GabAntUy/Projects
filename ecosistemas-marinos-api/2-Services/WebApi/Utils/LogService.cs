using ApplicationLogic.Interfaces;
using BusinessLogic.Entities;
using WebApi.Utils.Interfaces;

namespace WebApi.Utils
{
    public class LogService : ILogService
    {
        private ICreate<Log> _log;
        private IUserNameService _userNameService;

        public LogService(ICreate<Log> log, IUserNameService userNameService)
        {
            _log = log;
            _userNameService = userNameService;

        }

        public void CreateLog(int idEntidad, string tipoEntidad)
        {
            var log = new Log
            {
                UserName = _userNameService.GetActualUsername(),
                IdEntidad = idEntidad,
                TipoEntidad = tipoEntidad
            };
            try
            {
                _log.Create(log);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
