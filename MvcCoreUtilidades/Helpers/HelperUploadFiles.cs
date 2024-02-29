namespace MvcCoreUtilidades.Helpers
{
    public class HelperUploadFiles
    {
        private HelperPathProvider helperPath;

        public HelperUploadFiles(HelperPathProvider helperPath)
        {
            this.helperPath = helperPath;
        }

        public async Task<string> UploadFileAsync(IFormFile file, Folders folder)
        {
            string filename = file.FileName;

            //recuperamos la ruta
            string path =
                this.helperPath.MapPath(filename, folder);

            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return path;
        }
    }
}
