namespace Wally.RomMaster.Domain.Models
{
    public class FileQueueItem
    {
        public string File { get; set; }

        public override string ToString()
        {
            return File;
        }
    }
}
