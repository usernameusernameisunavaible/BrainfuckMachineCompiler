using System.Windows;
using System.Windows.Documents;
using Microsoft.Win32;


namespace BrainfuckMachineCompiler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly TextRange input;
        private readonly TextRange output;
        private readonly OpenFileDialog fileDialog = new();
        private string memorized_path;

        public MainWindow()
        {
            InitializeComponent();

            memorized_path = string.Empty;
            input = new(IDE_Main_Text.Document.ContentStart, IDE_Main_Text.Document.ContentStart.DocumentEnd);
            output = new(ConsoleOut.Document.ContentStart, ConsoleOut.Document.ContentStart.DocumentEnd);

            //the memorized path will be set to the parameter which is given, 
            //if that's not happening the default is "new file"
            Title = memorized_path == string.Empty ? "new file" : memorized_path;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            output.Text = IAssembler.Translate(input.Text);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ///<summary>
            ///Opens file over the filedialog and sets the title to the filename.
            /// </summary>
            fileDialog.InitialDirectory = memorized_path == string.Empty?"c:\\" : memorized_path;
            if((bool)fileDialog.ShowDialog())
            {
                memorized_path = fileDialog.FileName;
                input.Text = FileAbstractions.LoadText(fileDialog.FileName);
            }

            //remembers the path of the chosen file, sets title to it since it's the uptodate one
            this.Title = memorized_path;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if(memorized_path == string.Empty)
            {
                if((bool)fileDialog.ShowDialog())
                {
                    this.Title = memorized_path = fileDialog.FileName;
                }
                else
                {
                    return;
                }
            }
            FileAbstractions.SaveText(memorized_path, input.Text);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //compile

            if (memorized_path == string.Empty)
            {
                if ((bool)fileDialog.ShowDialog())
                {
                    this.Title = memorized_path = fileDialog.FileName;
                }
                else
                {
                    return;
                }
            }
            FileAbstractions.SaveBytes(
                memorized_path,
                Compiler.Compile(input.Text));
        }
    }
}
