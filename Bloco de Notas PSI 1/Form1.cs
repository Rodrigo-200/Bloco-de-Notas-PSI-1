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
             * FullName comporta-se como um full path do ficheiro/diretorio ja o name é so o nome do ficheiro/diretorio
             */

            txt_Path.Text = path; //Mostra o path
            DirectoryInfo dir = new DirectoryInfo(path);
            foreach (var item in dir.GetDirectories()) // Vai a cada diretorio dentro do diretorio "path"
            {
                //podemos adicionar a um controlo como uma listview
                //Exempo
                listView1.Items.Add(new ListViewItem(item.Name, 0));
            }
            foreach (var item in dir.GetFiles()) // Vai a cada diretorio dentro do diretorio "path" e vai buscar todos os ficheiros
            {
                //podemos adicionar a um controlo como uma listview
                listView1.Items.Add(new ListViewItem(item.Name, 1));
            }

            /*
             * Podemos nao ter acesso a alum diretorio logo a importancia dos try... catch
             */

            path = path += listView1.SelectedItems[0].Text + @"\";

            //back

            path = new DirectoryInfo(path).Parent.FullName;

            dir.Attributes = FileAttributes.Hidden; //hidden files.

        }

        private void button1_Click(object sender, EventArgs e)
        {
            StreamWriter_StreamReader_BinaryWriter_BinaryReader();
        }




        private void StreamWriter_StreamReader_BinaryWriter_BinaryReader()
        {
            //Leitura e escrita de ficheiros 

            //Criação e escrita de ficheiros com bytes (apenas em bytes!!!)

            //Instenciar um objeto do tipo stream e o unico campo obrigatório é o caminho (absoluto)
            FileStream f = new FileStream("Numbers.txt", FileMode.Create, FileAccess.Write, FileShare.None); 

            //Para escrever coisas é preciso converter para bytes e "meter lá para dentro"
            byte[] tesss = new UTF8Encoding().GetBytes("Teste escrever em ficheiro");

            //bytes, inicio, lenght...
            f.Write(tesss, 3, tesss.Length - 3);

            for(int i = 0; i < 10; i++)
            {
                byte[] number = new UTF8Encoding().GetBytes(i.ToString());
                f.Write(number, 0, number.Length);
            }
            f.Close(); //Se não fecharmos depois dá erro a dizer que está em utilização


            FileStream fbinary = new FileStream("Numbers.txt", FileMode.Create, FileAccess.Write, FileShare.None);

            BinaryWriter bw = new BinaryWriter(fbinary, Encoding.UTF8);
            bw.Write("INETE");
            bw.Write(true);
            bw.Write(1);
            bw.Write(12);
            bw.Write(14);
            bw.Write(12);
            bw.Close();


            FileStream fbinaryread = new FileStream("Numbers.txt", FileMode.Open);
            BinaryReader br = new BinaryReader(fbinaryread, Encoding.ASCII);
            string str = br.ReadString();
            bool bol = br.ReadBoolean();
            int k = br.ReadInt32();
            int a = br.ReadInt32();
            int b = br.ReadInt32();
            int c = br.ReadInt32();
            int rr = br.ReadInt32(); //O programa rebenta porque vai ler "nada (null)" metemos aqui propositalmente para ver o erro
            br.Close();

            // --------------------------------- //

            //StreamWriter trabalha com texto
            //Write não cria uma nova linha writelinha cria uma nova linha
            StreamWriter sw2 = new StreamWriter("comment.txt");
            sw2.WriteLine("INETE" + Environment.NewLine + "Escola");
            sw2.Write(true);
            sw2.Write(1);
            sw2.Close();

            //StreamReader 
            StreamReader sr = new StreamReader("comment.txt");
            //Ler linha a linha
            while (!sr.EndOfStream)
                MessageBox.Show(sr.ReadLine());
            sr.Close();



            sr = new StreamReader("Numbers.txt");
            //Ler caracter a caracter
            while (!sr.EndOfStream)
                MessageBox.Show(((char)sr.Read()).ToString());
            sr.Close();


            sr = new StreamReader("comment.txt");
            //Ler Tudo
                MessageBox.Show(sr.ReadToEnd());
            sr.Close();


            //Trabalhar com strings em vez de ficheiros

            var text = @"Olá
                         olé
                         fixe";



            //Com isto podemos ler linha a linha com as quebras de texto já que se fosse só uma string não teria as quebras de linha
            StringReader srd = new StringReader(text);
            string line;

            while((line = srd.ReadLine()) != null)
            {
                MessageBox.Show(line);
            }
        }

        private void XML()
        {
            //Ver alunos.xml serve para transmitir informações

            //Pesquisar Grid Systems

            //Exemplos BootStrap

            // Web Service

            //Usa-se para comunicar com uma base de dados de maneira mais segura, serve de intermediario entre as conexões/comunicações

            // Um site responsive adapta-se as dimensões do ecrã do dispositivo

        }
    }
}
