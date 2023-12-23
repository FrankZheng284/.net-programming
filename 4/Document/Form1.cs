using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Document
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeTreeView();
            InitializeListView();
        }


        private void InitializeTreeView()
        {
            string[] drives = Directory.GetLogicalDrives();
            foreach (string drive in drives)
            {
                TreeNode driveNode = new TreeNode(drive);
                driveNode.Tag = drive; 
                treeView.Nodes.Add(driveNode);
                PopulateTreeView(drive, driveNode);
            }
        }

        private void PopulateTreeView(string path, TreeNode parentNode)
        {
            string[] folders = Directory.GetDirectories(path);
            foreach (string folder in folders)
            {
                TreeNode node = new TreeNode(Path.GetFileName(folder));
                node.Tag = folder; 
                parentNode.Nodes.Add(node);
            }
        }

        private void InitializeListView()
        {
            listView.View = View.Details;
            listView.Columns.Add("Name", 200);
            listView.Columns.Add("Type", 80);

            treeView.AfterSelect += new TreeViewEventHandler(treeView_AfterSelect);
            listView.DoubleClick += new EventHandler(listView_DoubleClick);
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            listView.Items.Clear();
            string selectedPath = e.Node.Tag.ToString();

            string[] subDirectories = Directory.GetDirectories(selectedPath);
            string[] files = Directory.GetFiles(selectedPath);

            foreach (string folder in subDirectories)
            {
                ListViewItem item = new ListViewItem(Path.GetFileName(folder));
                item.SubItems.Add("文件夹");
                listView.Items.Add(item);
            }

            foreach (string file in files)
            {
                ListViewItem item = new ListViewItem(Path.GetFileName(file));
                item.SubItems.Add("文件");
                listView.Items.Add(item);
            }
        }

        private void listView_DoubleClick(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
            {
                string selectedItem = listView.SelectedItems[0].Text;
                string selectedPath = Path.Combine(treeView.SelectedNode.Tag.ToString(), selectedItem);

                if (File.Exists(selectedPath))
                {
                    if (selectedItem.ToLower().EndsWith(".exe"))
                    {
                        Process.Start(selectedPath);
                    }
                    else if (selectedItem.ToLower().EndsWith(".txt"))
                    {
                        Process.Start("notepad.exe", selectedPath);
                    }
                }
            }
        }
    }
}
