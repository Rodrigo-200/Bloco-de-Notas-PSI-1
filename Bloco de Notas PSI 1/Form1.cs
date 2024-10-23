using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO; // De onde vem o file e fileinfo etc (Importante saber!!)

namespace Bloco_de_Notas_PSI_1
{
    public partial class Form1 : Form
    {
        FileInfo testefileinfo;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*
             * Bloco de notas PSI 1
             * Processamento de ficheiros 
             * 23/10/2024
             */

            string path = @"c:\";

            /*
             * File e FileInfo trata de ficheiros
             * Dir e DirInfo
             */

            /*
             * File não é instanciado logo nao fazemos por exemplo (file name = new file) fazemos (File.alguma coisa)
             */

            if (!Directory.Exists("veiculos")) //Se esta pasta não existir criar a pasta 
                Directory.CreateDirectory("veiculos"); // Cria uma nova pasta (Neste caso com o nome veiculos)

            //@Serve para ignorar barras nas strings assim pode se fazer so 1 barra logo --> @"veiculos\teste.txt"

            //Exemplo de um try catch
            try
            {
                File.Create(@"veiculos\teste.txt"); // --> Este ficheiro e diretorio sao criados na pasta de onde o exe esta a ser corrido este ficheiro é adicionado dentro da pasta veiculos.
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                throw;
            }

            //Importante usar try... Catch

            /*
             * FileInfo
             */
            testefileinfo = new FileInfo(@"veiculos\teste.txt"); //Este fileinfo returna todas as informações sobre o ficheiro

            //.dispose faz "dispose" do ficheiro

            /*
             * Apagar ficheiros
              
             * "variavel".delete
             
             * Mover ficheiros
             * "variavel".MoveTo
             */

            /*
             * DirectoryInfo retorna toda a infrmação de um diretorio
             */

            


        }
    }
}
