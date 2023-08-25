namespace bcw_2023summer_keepr.Services
{
    public class KeepsService
    {
        private readonly KeepsRepository _keepsRepository;

        public KeepsService(KeepsRepository keepsRepository)
        {
            _keepsRepository = keepsRepository;
        }

        internal Keep CreateKeep(Keep keepData)
        {
            int keepId = _keepsRepository.CreateKeep(keepData);
            Keep createdKeep = GetKeepById(keepId);
            return createdKeep;
        }

        internal void DeleteKeep(int keepId, string userId)
        {
            Keep originalKeep = GetKeepById(keepId);
            if (originalKeep.CreatorId != userId)
            {
                throw new Exception($"No keep found with that id!");
            }
            _keepsRepository.DeleteKeep(keepId);
        }

        internal Keep EditKeep(Keep keepData, string userId)
        {
            Keep originalKeep = GetKeepById(keepData.Id);
            if (originalKeep.CreatorId != userId)
            {
                throw new Exception($"No keep found with that id!");
            }
            originalKeep.Name = keepData.Name ?? originalKeep.Name;
            originalKeep.Description = keepData.Description ?? originalKeep.Description;
            _keepsRepository.EditKeep(originalKeep);
            Keep editedKeep = GetKeepById(keepData.Id);
            return editedKeep;
        }

        internal Keep GetKeepById(int keepId)
        {
            Keep keep = _keepsRepository.GetKeepById(keepId);
            if (keep == null || keep.Id == 0)
            {
                throw new Exception($"No keep found with that id!");
            }
            return keep;
        }

        internal List<Keep> GetKeeps()
        {
            List<Keep> keeps = _keepsRepository.GetKeeps();
            return keeps;
        }
    }
}