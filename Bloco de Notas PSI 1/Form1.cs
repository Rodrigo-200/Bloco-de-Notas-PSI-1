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
using System.Xml;
using Newtonsoft.Json;

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
            //StreamWriter_StreamReader_BinaryWriter_BinaryReader();
            XML();
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

            for (int i = 0; i < 10; i++)
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

            while ((line = srd.ReadLine()) != null)
            {
                MessageBox.Show(line);
            }
        }

        private void XML()
        {
            /*
            Ver alunos.xml serve para transmitir informações

            Pesquisar Grid Systems

            Exemplos BootStrap

             Web Service

            Usa-se para comunicar com uma base de dados de maneira mais segura, serve de intermediario entre as conexões/comunicações

            Um site responsive adapta-se as dimensões do ecrã do dispositivo 
            */

            //Não se faz desta maneira mas o professor mostra para sabermos

            //string xmlpath = @"C:\Inete\INETE 2022\testexmlserialize.xml";
            // Lista de dados exemplo
            List<Aluno> alunos = new List<Aluno>();
            alunos.Add(new Aluno { Numero = 1, Nome = "Luis Martins", Media = 18.5F });
            alunos.Add(new Aluno { Numero = 2, Nome = "Maria Antonieta", Media = 15F });
            alunos.Add(new Aluno { Numero = 3, Nome = "Jaquim Manel" });
            /*


                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(alunos.GetType());

                // Create a new StreamWriter
                TextWriter writer = new StreamWriter(xmlpath);

                serializer.Serialize(writer, alunos);

                // Close the writer
                writer.Close();

                // Create a StreamReader
                StreamReader reader2 = new StreamReader(xmlpath);

                // Deserialize the file
                List<Aluno> Alunosdesc;
                Alunosdesc = (List<Aluno>)serializer.Deserialize(reader2);

                // Close the reader
                reader2.Close();
            */

            //De aqui para baixo já podemos fazer.
            //Existem Elementos e atributos
            //Elementos é as "tags" exemplo <Professor></Professor>
            //Atributos mete-se dentro da Tag exemplo <Professor numero="1"></Professor>
            //Elementstring abre e fecha logo o elemento na mesma linha


            string nomeXML = @"alunos.xml";

            // settings da escrita do ficheiros
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            //Serve para fazer por exemplo os "tabs" no codigo fica identado (mais giro)
            xmlSettings.Indent = true; //Fica autmaticamente identado
            xmlSettings.IndentChars = " ";

            XmlWriter xmlOut = XmlWriter.Create(nomeXML, xmlSettings);
            //Começa o documento
            xmlOut.WriteStartDocument(); // escrita da XML declaration
            xmlOut.WriteComment("Informação de alunos"); // Escreve um comentario
            //Criamos um elemento "start" ou seja ( <Escola> )  
            xmlOut.WriteStartElement("Escola"); // escrita do root element
            //Cria outro elemento "Start" ( <Alunos> ) por agora esta ( <Escola> <Alunos> )
            xmlOut.WriteStartElement("Alunos");
            float media = 0;
            // Percorremos os alunos todos para os adicionar

            foreach (Aluno a in alunos)
            {
                media += a.Media;
                //Cria outro elemento "Start" ( <Aluno> ) por agora esta ( <Escola> <Alunos> <Aluno> )
                xmlOut.WriteStartElement("Aluno");
                //Olha para o elemnto aberto anterior (StartElement) e vai escrever o numero assim (Numero "numero") por agora esta ( <Escola> <Alunos> <Aluno Numero: x> )
                xmlOut.WriteAttributeString("Número", a.Numero.ToString());
                //Cria um elemento "String" ( <nome></nome> ) por agora ficaria ( <Escola> <Alunos> <Aluno Numero: x> <nome> xxxx </nome> )
                xmlOut.WriteElementString("Nome", a.Nome);
                //Igual ao de cima
                xmlOut.WriteElementString("Média", a.Media.ToString());
                //Fecha o elemento "Aluno" logo ficaria ( <Escola> <Alunos> <Aluno Numero: x> <nome> xxxx </nome> <Média> x </Média> </Aluno> )
                xmlOut.WriteEndElement();
            }

            xmlOut.WriteEndElement(); // escrita da end tag Alunos ficaria ( <Escola> <Alunos> <Aluno Numero: x> <nome> xxxx </nome> <Média> x </Média> </Aluno> </Alunos> )

            //Igual aos Alunos
            xmlOut.WriteStartElement("Professores");
            foreach (Aluno a in alunos)
            {
                media += a.Media;
                xmlOut.WriteStartElement("Professor");
                xmlOut.WriteElementString("Nome", a.Nome);
                xmlOut.WriteEndElement();
            }

            xmlOut.WriteEndElement(); // Fecha a tag Professores

            xmlOut.WriteStartElement("Informações");
            media = media / alunos.Count;
            xmlOut.WriteElementString("MédiaGlobal", media.ToString());

            xmlOut.WriteEndElement();
            xmlOut.WriteElementString("professor", "Paulo Jorge");
            xmlOut.WriteEndElement(); //Fecha a tag root Escola ( <Escola> ... </Escola> )
            xmlOut.Close(); // fechar o objecto


            // --------------------------------------------------------------------------------------- //

            //Ler ficheiros XML
            string conteudo = "";

            XmlReaderSettings xmlSettingsleitura = new XmlReaderSettings();

            xmlSettingsleitura.IgnoreComments = true;
            xmlSettingsleitura.IgnoreWhitespace = true;
            XmlReader reader = XmlReader.Create(nomeXML, xmlSettingsleitura);
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // The node is an element.
                        if (reader.Name == "Aluno")
                        {
                            conteudo = reader.GetAttribute("Número");
                            reader.Read();
                            reader.Read();
                            MessageBox.Show(reader.Value);
                            reader.Read();
                            reader.Read();
                            reader.Read();
                            MessageBox.Show(reader.Value);
                        }
                        if (reader.Name == "Professor")
                        {
                            reader.Read();
                            reader.Read();
                            conteudo += reader.Value;
                        }
                        break;

                    case XmlNodeType.Text: //Display the text in each element.
                        conteudo += reader.Value;
                        break;

                    case XmlNodeType.Attribute: //Display the end of the element.
                        conteudo += reader.Value;
                        break;
                }
                MessageBox.Show(conteudo);
            }
            reader.Close();

            List<Aluno> alunosler = new List<Aluno>();
            // settings da forma de leitura do ficheiro
            xmlSettingsleitura.IgnoreComments = true;
            xmlSettingsleitura.IgnoreWhitespace = true;
            XmlReader xmlIn = XmlReader.Create(nomeXML, xmlSettingsleitura);
            // procurar o primeiro elemento de tag "Aluno"
            if (xmlIn.ReadToDescendant("Aluno") == true)
            {
                do
                {
                    Aluno a = new Aluno();
                    a.Numero = Convert.ToInt32(xmlIn["Número"]); // ler o atributo
                    xmlIn.ReadStartElement("Aluno"); // ler o elemento Aluno
                    a.Nome = xmlIn.ReadElementContentAsString(); // obter o nome
                                                                 // obter a média
                    a.Media = Convert.ToSingle(xmlIn.ReadElementContentAsString());
                    alunosler.Add(a); // adicionar o aluno à lista
                } while (xmlIn.ReadToNextSibling("Aluno"));
            }

            xmlIn
                .Close();
            // fechar o obj


            // --------------------------------------------------------------------------------- //


            ///Editar/Ler ficheiros XML 

            XmlDocument doc = new XmlDocument();
            doc.Load(nomeXML);

            //Get a list of nodes - in this case, I'm selecting all <Aluno> nodes under
            XmlNodeList aNodes = doc.SelectNodes("/Escola/Alunos/Alunos");

            //loop through all alunos nodes

            foreach (XmlNode aNode in aNodes)
            {
                XmlAttribute idAttribute = aNode.Attributes["Numero"];
                if (idAttribute.Value == "2")
                {
                    aNode.ParentNode.RemoveChild(aNode);
                }
                else
                {
                    XmlElement moradaelement = doc.CreateElement("morada");
                    moradaelement.InnerText = "Morada " + idAttribute.Value;
                    aNode.AppendChild(moradaelement);

                    //grab the "id" attribute
                    if (idAttribute.Value == "1")
                    {
                        aNode.SelectSingleNode("Nome").InnerText = "Paulo";
                    }
                    //Check if that attribute even exists

                    if (idAttribute != null)
                    {
                        //If yes - read its current value 
                        string currentValue = idAttribute.Value;

                        if (string.IsNullOrEmpty(currentValue))
                        {
                            idAttribute.Value = "0";
                        }
                    }
                }
            }
            // Save the XmlDocument back to Disk 
            //Ao dar load the um ficheiro como doc nao prende o ficheiro logo podemos usa-lo ou guardar em outro caminho
            doc.Save(nomeXML);



        }

        private void Json()
        {
            List<Aluno> alunos = new List<Aluno>;
            alunos.Add(new Aluno { Numero = 1, Nome = "Luis Martins", Media = 18.5F });
            string jsonalunos = JsonConvert.SerializeObject(alunos);
            button1.Text = jsonalunos;

            var alunosjson = JsonConvert.DeserializeObject<List<Aluno>>(jsonalunos);
            dynamic array = JsonConvert.DeserializeObject(jsonalunos);
            foreach(var item in array)
            {
                Console.WriteLine("{0} {1}", item.Nome, item.Media);
            }

        }


    }
}