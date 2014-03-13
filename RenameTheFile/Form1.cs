using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace RenameTheFile
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        class deleteTag
        {
            public string[] files;

            public void renameTag(string tags)                                                          //tags - tag for delete
            {
                try
                {
                    if (files.Length != 0)
                    {                         
                        string pathDirectory;                                                           //Directory
                        string nameFile;                                                                //Name of Filr
                        int cutFirstIndex;                                                              //Index to check the tag in the word
                        int ind = 0;                                                                    //Index last slash

                        for (int i = 0; i < files.Length; i++)
                        {
                            cutFirstIndex = files[i].IndexOf(tags);                                     //check it for membership tags
                            if (cutFirstIndex != -1)                                                    //if there is a tag in the word
                            {
                                ind = files[i].LastIndexOf('\\');                                       //Search index last slash
                                pathDirectory = files[i].Substring(0, (ind + 1));                       //a path to the folder with the files
                                nameFile = files[i].Substring(ind + 1, (files[i].Length - (ind + 1)));  //get the file name
                                cutFirstIndex = nameFile.IndexOf(tags);                                 //take the current position of the first character of the tag
                                nameFile = nameFile.Remove(cutFirstIndex, tags.Length);                 //delete tag
                                File.Move(files[i], pathDirectory + nameFile);                          //finish renaming
                            }
                        }
                        MessageBox.Show("Completed!");
                        
                    }
                }
                catch (System.NullReferenceException)
                {
                    MessageBox.Show("Not specified the correct way!!");
                    return;
                }
                catch (System.IO.FileNotFoundException)
                {
                    MessageBox.Show("No files found!");
                    return;
                }
            }
        }

        deleteTag delTag = new deleteTag();

        public void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK)
            {

                delTag.files = Directory.GetFiles(fbd.SelectedPath);                                     //Get all files in folder
                /*
                 * contents of the array files:
                 * files[0] = C:\ff\text.txt
                 * files[1] = C:\ff\text(2).txt
                 */
                int ind = delTag.files[0].LastIndexOf('\\');                                             //Search index last slash
                string pathDirectory = delTag.files[0].Substring(0, (ind + 1));                         //a path to the folder with the files
                textBox1.Text = pathDirectory;  
            }
        }

        public void button2_Click(object sender, EventArgs e)
        {
            string tags;
            tags = textBox2.Text;
            if (tags.Length > 1 && tags != "")
            {
                delTag.renameTag(tags);

            }
            else MessageBox.Show("Incorrect tag!");
        }

        

        
    }
}
