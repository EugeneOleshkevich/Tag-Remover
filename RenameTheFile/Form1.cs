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
//Добавить удаление тегов через запятую
namespace RenameTheFile
{
    public partial class Form1 : Form
    {
        class deleteTag
        {
            public string[] files;
            public string[] tag;
            public string nameFile;
            FileInfo file;
            public void renameTag()                                                                             //tags - tag for delete
            {
                try
                {
                    if (string.IsNullOrEmpty(files[0]) == false)
                    {
                        string pathDirectory;                                                                   //Directory
                        string nameFile;                                                                        //Name of Filr
                        int cutFirstIndex;                                                                      //Index to check the tag in the word
                        int ind = 0;                                                                            //Index last slash
                        int j = 0;

                        do
                        {
                            for (int i = 0; i < files.Length; i++)
                            {
                                if (string.IsNullOrEmpty(tag[j]) == false)
                                    break;
                                cutFirstIndex = files[i].IndexOf(tag[j]);                                       //check it for membership tags
                                if (cutFirstIndex != -1)                                                        //if there is a tag in the word
                                {
                                    ind = files[i].LastIndexOf('\\');                                           //Search index last slash
                                    pathDirectory = files[i].Substring(0, (ind + 1));                           //a path to the folder with the files
                                    nameFile = files[i].Substring(ind + 1, (files[i].Length - (ind + 1)));      //get the file name
                                    cutFirstIndex = nameFile.IndexOf(tag[j]);                                   //take the current position of the first character of the tag
                                    nameFile = nameFile.Remove(cutFirstIndex, tag[j].Length);                   //delete tag
                                    File.Move(files[i], pathDirectory + nameFile);                              //finish renaming
                                }

                            }

                            j++;
                        } while (string.IsNullOrEmpty(tag[j]) == false);
                        MessageBox.Show("Completed!");

                    }
                }
                catch (System.IO.FileNotFoundException)
                {
                    MessageBox.Show("No files found!");
                    return;
                }
                catch (System.NullReferenceException)
                {
                    MessageBox.Show("Folder is not selected!");
                    return;
                }
            }
            public void formatedTags(string inputMoreTag)                                                   //function for adding tags to an array tag
            {
                int startIndex = 0;
                int cutIndex;                                                                               //check listing
                string changeInputTags = inputMoreTag;

                if (inputMoreTag.Length < 2)                                                                //if a small tag length
                {
                    return;
                }

                cutIndex = changeInputTags.IndexOf(' ');                                                    //checking for gaps
                if (cutIndex != -1)                                                                         //there are gaps
                {
                    do
                    {

                        changeInputTags = changeInputTags.Remove(cutIndex, 1);                              //remove the space
                        cutIndex = changeInputTags.IndexOf(' ');                                            //looking for new
                    } while (cutIndex != -1);                                                                //runs until all spaces removed
                }

                cutIndex = changeInputTags.IndexOf(',');                                                    //looking through the enumeration tags
                int i = 0;
                int lenth;
                try
                {

                    if (cutIndex != -1)                                                                     //if found comma
                    {
                        do
                        {
                            lenth = changeInputTags.Length;
                            if (cutIndex == -1)
                            {
                                tag[i] = changeInputTags.Substring(startIndex, changeInputTags.Length);     //catch the last tag for the last comma
                                return;
                            }
                            tag[i] = changeInputTags.Substring(startIndex, cutIndex);                       //We bring in an array of array-tag
                            changeInputTags = changeInputTags.Remove(startIndex, (cutIndex + 1));           //remove the tag that we have brought
                            cutIndex = changeInputTags.IndexOf(',');                                        //looking for a new comma
                            i++;                                                                            //transition to the next line array-tags
                            if (changeInputTags == ",")                                                     //if there will be only a comma and it no tag
                                break;
                        } while (i < tag.Length || changeInputTags == "" || cutIndex != -1);                //running until it runs in the array tags, or until it finds a blank line
                    }
                    else
                    {
                        tag[0] = changeInputTags.Substring(startIndex, changeInputTags.Length);                      //only one tag
                    }
                }
                catch (System.ArgumentOutOfRangeException)
                {
                    MessageBox.Show("Going beyond!");
                }
            }
            public string startProgrammTag()
            {
                if (file.Exists == false)                                           
                {

                    StreamWriter write_text;                                          
                   
                    write_text = file.AppendText();                         
                    write_text.WriteLine("(muzofon.com), myzuka.ru_");      
                    write_text.Close();                                     
                }
                StreamReader streamReader = new StreamReader(nameFile);     
                string str = "";                                            

                while (!streamReader.EndOfStream)                           
                {
                    str += streamReader.ReadLine();                         
                }
                streamReader.Close();
                return str;
            }                                                              //function to create a default file if it is not and tag reading from it
            public void saveTags(string writeTags)
            {
                File.WriteAllText(nameFile, writeTags);
            }                                                          //save tags in a file, which are text box
            public deleteTag()
            {
                nameFile = "FrequentlyUsedTags.txt";
                tag = new string[10];
                file = new FileInfo(nameFile);
            }

        }

        deleteTag delTag = new deleteTag();

        public Form1()
        {
            InitializeComponent();
            textBox2.Text = delTag.startProgrammTag();   
        }

        
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
            delTag.formatedTags(textBox2.Text);
            delTag.renameTag();
        }

        public void button3_Click(object sender, EventArgs e)
        {
            delTag.saveTags(textBox2.Text);      
        }
   
    }
}
