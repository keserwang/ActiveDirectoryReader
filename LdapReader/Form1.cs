using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Security.Principal;
using System.Collections;
using System.DirectoryServices.Protocols;
using System.Net;
using LdapAndAdLibrary;
using System.Collections.Specialized;

namespace LdapReader
{
    public partial class Form1 : Form
    {
        DataTable dtAttribute { get; set; }

        //Dictionary<string, List<AttributeDataModel>> attributeDictionary { get; set; }

        LdapHelper ldapHelper;


        public Form1()
        {
            InitializeComponent();

            try
            {
                textBoxPath.Text = ConfigurationManager.AppSettings["Path"];
                textBoxAccount.Text = ConfigurationManager.AppSettings["Account"];
                textBoxPassword.Text = ConfigurationManager.AppSettings["Password"];
                textBoxDn.Text = ConfigurationManager.AppSettings["DistinguishedName"];

                ldapHelper = new LdapHelper(textBoxPath.Text, textBoxAccount.Text, textBoxPassword.Text);

                dtAttribute = new DataTable();
                dtAttribute.Columns.Add("Name");
                //dtAttribute.Columns.Add("Type");
                dtAttribute.Columns.Add("Value");
                dataGridViewAttribute.DataSource = dtAttribute;

                //attributeDictionary = new Dictionary<string, List<AttributeDataModel>>();
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

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            try
            {
                richTextBoxMessage.Clear();

                ldapHelper.Login(textBoxAccount.Text, textBoxPassword.Text);
                richTextBoxMessage.AppendText("登入成功");
            }
            catch (Exception ex)
            {
                richTextBoxMessage.AppendText("登入失敗" + Environment.NewLine);
                richTextBoxMessage.AppendText(ex.ToString());
            }
            finally
            {
                richTextBoxMessage.ScrollToCaret();
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                richTextBoxMessage.Clear();

                ResultCode result = ldapHelper.Delete(textBoxDn.Text);
                if (result == ResultCode.Success)
                {
                    richTextBoxMessage.AppendText("刪除成功");
                }
                else
                {
                    richTextBoxMessage.AppendText("刪除失敗");
                    richTextBoxMessage.AppendText(result.ToString());
                }
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

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            try
            {
                // Display a wait cursor while the TreeNodes are being created.
                Cursor.Current = Cursors.WaitCursor;

                richTextBoxMessage.Clear();
                dtAttribute.Clear();

                FillTreeView();
            }
            catch (Exception ex)
            {
                richTextBoxMessage.AppendText(ex.ToString());
            }
            finally
            {
                richTextBoxMessage.ScrollToCaret();

                // Reset the cursor to the default for all controls.
                Cursor.Current = Cursors.Default;
            }
        }

        private void FillTreeView()
        {
            treeView1.BeginUpdate();
            treeView1.Nodes.Clear();
            string ldapFilter = string.IsNullOrWhiteSpace(textBoxFilter.Text) ? null : textBoxFilter.Text;
            Dictionary<string, List<AttributeDataModel>> result = ldapHelper.Search(textBoxDn.Text, ldapFilter, searchScope: SearchScope.Base);

            //Dictionary<string, List<AttributeDataModel>> result = ldapHelper.Search(textBoxDn.Text, searchScope: SearchScope.Base);

            int resultCount = 0;
            foreach (var item in result)
            {
                TreeNode treeNodeRoot = treeView1.Nodes.Add(item.Key);
                treeNodeRoot.Tag = item.Value;
                resultCount++;
                resultCount += searchChildren(treeNodeRoot);
            }

            treeView1.EndUpdate();
            treeView1.ResumeLayout();
            labelResultCount.Text = resultCount.ToString();
        }

        private int searchChildren(TreeNode parent)
        {
            Dictionary<string, List<AttributeDataModel>> children = ldapHelper.Search(parent.Text, searchScope: SearchScope.OneLevel);

            int resultCount = children.Count;
            for (int i = 0; i < children.Count; i++)
            {
                KeyValuePair<string, List<AttributeDataModel>> child = children.ElementAt(i);

                TreeNode treeNodeChild = parent.Nodes.Add(child.Key);
                treeNodeChild.Tag = child.Value;
                resultCount += searchChildren(treeNodeChild);
            }

            return resultCount;
        }

        /// <summary>
        /// 將 TreeNode.Tag 中的資料帶入 dataGridViewAttribute。
        /// 因為 dataGridViewAttribute.DataSource = dtAttribute，所以將資料帶入 dtAttribute 即可。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            dtAttribute.Clear();

            List<AttributeDataModel> attributeList = e.Node.Tag as List<AttributeDataModel>;

            if (attributeList == null)
            {
                return;
            }

            foreach (var item in attributeList)
            {
                DataRow drAttribute = dtAttribute.NewRow();
                drAttribute["Name"] = item.Name;
                //drAttribute["Type"] = item.Type;
                drAttribute["Value"] = item.Value;
                dtAttribute.Rows.Add(drAttribute);
            }
        }
    }
}
