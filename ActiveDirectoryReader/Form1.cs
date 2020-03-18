using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.DirectoryServices;
using System.Configuration;
using System.Security.Principal;
using System.Collections;
using ActiveDirectoryLibrary;
using DirectoryLibrary;

namespace ActiveDirectoryReader
{
    public partial class Form1 : Form
    {
        DataTable dtAttribute { get; set; }

        public Form1()
        {
            InitializeComponent();
            
            dtAttribute = new DataTable();
            dtAttribute.Columns.Add("Name");
            dtAttribute.Columns.Add("Value");
            dataGridViewAttribute.DataSource = dtAttribute;

            textBoxPath.Text = ConfigurationManager.AppSettings["Path"];
            textBoxAccount.Text = ConfigurationManager.AppSettings["Account"];
            textBoxPassword.Text = ConfigurationManager.AppSettings["Password"];
        }

        private void buttonQueryAll_Click(object sender, EventArgs e)
        {
            try
            {
                richTextBoxMessage.Clear();

                ActiveDirectoryHelper adHelper = new ActiveDirectoryHelper(textBoxPath.Text, textBoxAccount.Text, textBoxPassword.Text);
                SearchResultCollection results = adHelper.FindAll();

                if (results == null)
                {
                    richTextBoxMessage.AppendText("No data.");
                    return;
                }

                foreach (SearchResult result in results)
                {
                    ResultPropertyCollection rpc = result.Properties;

                    List<string> nameList = new List<string>(rpc.PropertyNames.Count);
                    foreach (string name in rpc.PropertyNames)
                    {
                        nameList.Add(name);
                    }
                    nameList = nameList.Distinct().OrderBy(x => x).ToList();

                    foreach (string name in nameList)
                    {
                        ResultPropertyValueCollection rpvc = rpc[name];
                        int valueCount = rpvc.Count;
                        foreach (object value in rpvc)
                        {
                            if (value == null)
                                continue;

                            string valueString = DirectoryUtility.ExtractAttributValue(name, value);
                            richTextBoxMessage.AppendText(string.Format("{0}={1}\n", name, valueString));
                        }
                    }

                    richTextBoxMessage.AppendText("\n----------------------------------------------------------\n\n");
                    richTextBoxMessage.ScrollToCaret();
                }

                richTextBoxMessage.AppendText($"Count: {results.Count}");
            }
            catch (Exception ex)
            {
                richTextBoxMessage.AppendText(ex.ToString());
            }
            finally
            {
                richTextBoxMessage.ScrollToCaret();
            }
        }

        private void buttonQuery1_Click(object sender, EventArgs e)
        {
            try
            {
                richTextBoxMessage.Clear();
                dtAttribute.Clear();

                ActiveDirectoryHelper adHelper = new ActiveDirectoryHelper(textBoxPath.Text, textBoxAccount.Text, textBoxPassword.Text);
                SearchResult result = adHelper.FindOne();

                if (result == null)
                {
                    richTextBoxMessage.AppendText("No data.");
                    return;
                }

                ResultPropertyCollection rpc = result.Properties;

                List<string> nameList = new List<string>(rpc.PropertyNames.Count);
                foreach (string name in rpc.PropertyNames)
                {
                    nameList.Add(name);
                }
                nameList = nameList.Distinct().OrderBy(x => x).ToList();

                foreach (string name in nameList)
                {
                    ResultPropertyValueCollection rpvc = rpc[name];
                    int valueCount = rpvc.Count;
                    foreach (object value in rpvc)
                    {
                        if (value == null)
                            continue;

                        string valueString = DirectoryUtility.ExtractAttributValue(name, value);

                        richTextBoxMessage.AppendText(string.Format("{0}={1}\n", name, valueString));

                        DataRow drAttribute = dtAttribute.NewRow();
                        drAttribute["Name"] = name;
                        drAttribute["Value"] = valueString;
                        dtAttribute.Rows.Add(drAttribute);
                    }
                }

                dataGridViewAttribute.Sort(dataGridViewAttribute.Columns[0], ListSortDirection.Ascending);
            }
            catch (Exception ex)
            {
                richTextBoxMessage.AppendText(ex.ToString());
            }
            finally
            {
                richTextBoxMessage.ScrollToCaret();
            }
        }

        
    }
}
