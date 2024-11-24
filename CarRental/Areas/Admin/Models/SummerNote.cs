namespace CarRental.Areas.Admin.Models
{
    public class SummerNote
    {
        public SummerNote(string idEditor, bool loadLibary = true)
        {
            IDEditor = idEditor;
            LoadLibary = loadLibary;
        }
        public string IDEditor { get; set; }
        public bool LoadLibary { get; set; }
        public int Height { get; set; }
        public string toolBar { get; set; } = @"[
            ['style', ['style']], 
            ['font', ['bold', 'underline', 'clear']], 
            ['color', ['color']], 
            ['para', ['ul','ol','paragraph']], 
            ['table', ['table']], 
            ['insert', ['link', 'elfinderFiles', 'video', 'elfinder']], 
            ['view', ['fullscreen', 'codeview', 'help']]

        
        ]";
    }
}
