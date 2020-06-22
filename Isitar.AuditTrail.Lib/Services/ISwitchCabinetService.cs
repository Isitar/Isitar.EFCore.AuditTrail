namespace Isitar.AuditTrail.Lib.Services
{
    using System;
    using System.Threading.Tasks;
    using DataAccess.Dao;

    public interface ISwitchCabinetService
    {
        public  void AddSwitchCabinet(SwitchCabinet sc);
        public  void UpdateSwitchCabinet(Guid id, SwitchCabinet sc);
        public  void DeleteSwitchCabinet(Guid id);
        public  void AddPLC(Guid switchCabinetId, PLC plc);

        public SwitchCabinet FindById(Guid id);
    }
}