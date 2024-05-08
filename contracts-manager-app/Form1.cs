using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.InteropServices.Marshalling;
using System.IO;

namespace contracts_manager_app
{
    public partial class Form1 : Form
    {
        public DataTable contacts { get; set; }

        public DataTable searchedDT { get; set; }

        // �f�[�^�x�[�X�Ƃ̐ڑ�������쐬
        static string connectionString = @"Data Source = DSP407\SQLEXPRESS; Initial Catalog = contacts-manager-app; User ID = toru_yoshida; Password = 05211210; Encrypt = False; TrustServerCertificate=true";

        private Form2 f2;

        public string id { get; set; }
        public string name { get; set; }
        public string tel { get; set; }
        public string address { get; set; }
        public string remark { get; set; }

        public Form1()
        {
            InitializeComponent();

            // DataTable�̏�����
            contacts = new DataTable();
            contacts.Columns.Add("id", typeof(int));
            contacts.Columns.Add("name", typeof(string));
            contacts.Columns.Add("tel", typeof(string));
            contacts.Columns.Add("address", typeof(string));
            contacts.Columns.Add("remark", typeof(string));

            searchedDT = new DataTable();

            // ������Ԃ͐V�K�o�^

            f2 = new Form2(this);

            id = "";
            name = "";
            tel = "";
            address = "";
            remark = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // DataGridView�̏�����
            // �J���������w��
            dataGridView1.DataSource = contacts;

            // DataGridViewButtonColumn�̍쐬
            DataGridViewButtonColumn updateButton = new DataGridViewButtonColumn();
            DataGridViewButtonColumn deleteButton = new DataGridViewButtonColumn();

            // ��̖��O��ݒ�
            updateButton.Name = "�ҏW";
            deleteButton.Name = "�폜";

            // ���ׂẴ{�^����"�ҏW"�A"�폜"�ƕ\������
            updateButton.UseColumnTextForButtonValue = true;
            deleteButton.UseColumnTextForButtonValue = true;
            updateButton.Text = "�ҏW";
            deleteButton.Text = "�폜";

            // DataGridView�ɒǉ�����
            dataGridView1.Columns.Add(updateButton);
            dataGridView1.Columns.Add(deleteButton);

            // �J���������w��
            dataGridView1.Columns[1].HeaderText = "���O";
            dataGridView1.Columns[2].HeaderText = "�d�b�ԍ�";
            dataGridView1.Columns[3].HeaderText = "���[���A�h���X";
            dataGridView1.Columns[4].HeaderText = "���l";

            // �͂��߂̗���\���ɂ���
            dataGridView1.Columns[0].Visible = false;


            // �f�[�^�̒ǉ��e�X�g
            contacts.Rows.Add(1, "�c��", "123456789", "nanikasira", "�r�R�[");

            ScreenDisplay();

        }

        /// <summary>
        /// DataGridView�̕\��(��)�擾����
        /// </summary>
        public void ScreenDisplay()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // �\���̏�����
                    contacts.Clear();

                    // �e�[�u���̑S�v�f�擾�R�}���h�̐���
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT * FROM contacts";

                    // db�ڑ�
                    connection.Open();
                    Debug.WriteLine("�ڑ�����");

                    using (var sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                contacts.Rows.Add(sdr["id"], sdr["name"].ToString(), sdr["tel"].ToString(), sdr["address"].ToString(), sdr["remark"].ToString());
                            }
                        }
                    }

                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine("�װ" + ex.Message);
            }

        }

        /// <summary>
        /// �V�K�o�^�{�^���������̃C�x���g
        /// </summary>
        private void register_Click(object sender, EventArgs e)
        {
            id = "0";
            f2.update = false;
            f2.LabelChanger("�o�^");
            f2.ShowDialog();
        }


        /// <summary>
        /// �ҏW�A�폜�{�^�����������Ƃ��̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var dgv = (DataGridView)sender;
            // "�ҏW"�{�^�����������Ƃ��̏���
            if (dgv.Columns[e.ColumnIndex].Name == "�ҏW")
            {
                // �ҏW�A�X�V��ʂ̃{�^���̕\�L��"�X�V"�ɕύX
                f2.LabelChanger("�X�V");
                f2.update = true;
                // �����ꂽ"�ҏW"�{�^���̍s�̏��擾�A�i�[
                id = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                name = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                tel = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                address = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                if (dataGridView1.Rows[e.RowIndex].Cells[4].Value != null) remark = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();

                // �������擾�ł��Ă��邩�̃e�X�g
                // MessageBox.Show( id + name + tel + address + remark);

                // �J�ڐ�̉�ʂ̃e�L�X�g�{�b�N�X�Ɏ����I�ɓ���
                f2.TextBoxRegester(name, tel, address, remark);

                // ��ʑJ��
                f2.ShowDialog();
            }

            // "�폜"�{�^�����������Ƃ��̏���
            else if (dgv.Columns[e.ColumnIndex].Name == "�폜")
            {
                DialogResult result = MessageBox.Show("�A������폜���܂����H", "����",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);

                // OK���������Ƃ��̏���
                if (result == DialogResult.OK)
                {
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            // �폜�������s��id���擾
                            id = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                            // �N�G�����쐬
                            string cmdtest = "DELETE FROM contacts WHERE id = " + id;

                            using (var cmd = new SqlCommand(cmdtest, conn))
                            {
                                // db�ڑ�
                                conn.Open();
                                // �N�G�������s
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        // ��ʂ̍ĕ\��
                        ScreenDisplay();
                    }
                }
            }
        }

        /// <summary>
        /// �����{�^���������̃C�x���g
        /// </summary>
        private void search_Click(object sender, EventArgs e)
        {
            // �����񂪓��͂���Ă���ꍇ
            if (searchBox.Text.Length > 0)
            {
                // �f�[�^�Ƀt�B���^�[��������
                // �����̓e�L�X�g�{�b�N�X�̕����񂪖��O�A�d�b�ԍ��A���[���A�h���X�A���l�̂����ꂩ�̈ꕔ�������͑S���Ɉ�v
                contacts.DefaultView.RowFilter = "name LIKE '%" + searchBox.Text + "%' " +
                                                 "OR tel LIKE'%" + searchBox.Text + "%' " +
                                                 "OR address LIKE'%" + searchBox.Text + "%' " +
                                                 "OR remark LIKE'%" + searchBox.Text + "%' ";
            }
        }

        /// <summary>
        /// �G�N�X�|�[�g�{�^���������̃C�x���g
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void export_Click(object sender, EventArgs e)
        {
            if (contacts.Rows.Count > 0)
            {
                // �����擾
                DateTime dt = DateTime.Now;

                // csv�t�@�C���̃p�X�A�t�@�C�����͘A����_[�N�������ԕ�]
                string csvPath = @"C:\Users\toru_yoshida\source\repos\contracts-manager-app\�A����_"
                                    + dt.ToString("yyMMddHHmm") + ".csv";


                // CSV�t�@�C���ɏ������ނƂ��Ɏg��Encoding
                System.Text.Encoding enc = System.Text.Encoding.GetEncoding("Shift_JIS");

                // �������ރt�@�C�����J��
                StreamWriter sr = new StreamWriter(csvPath, false, enc);

                int colCount = contacts.Columns.Count;
                int lastColIndex = colCount - 1;

                // �w�b�_����������
                for (int i = 0; i < colCount; i++)
                {
                    // �w�b�_�̎擾
                    string field = contacts.Columns[i].Caption;
                    //"�ň͂�
                    field = EncloseDoubleQuotesIfNeed(field);
                    // �t�B�[���h����������
                    sr.Write(field);
                    // �J���}����������
                    if (lastColIndex > i)
                    {
                        sr.Write(',');
                    }

                }
                // ���s����
                sr.Write("\r\n");

                // ���R�[�h����������
                foreach (DataRow row in contacts.Rows)
                {
                    for (int i = 0; i < colCount; i++)
                    {
                        // �t�B�[���h�̎擾
                        string field = row[i].ToString();
                        // "�ň͂�
                        field = EncloseDoubleQuotesIfNeed(field);
                        // �t�B�[���h����������
                        sr.Write(field);
                        // �J���}����������
                        if (lastColIndex > i)
                        {
                            sr.Write(',');
                        }
                    }
                    // ���s����
                    sr.Write("\r\n");
                }
                sr.Close();

                MessageBox.Show("�G�N�X�|�[�g���܂���");
            }
            else
            {
                MessageBox.Show("�G�N�X�|�[�g����f�[�^�����݂��܂���");
            }
        }

        /// <summary>
        /// ��������_�u���N�H�[�g�ň͂�
        /// </summary>
        private string EncloseDoubleQuotesIfNeed(string field)
        {
            if (NeedEncloseDoubleQuotes(field))
            {
                return EncloseDoubleQuotes(field);
            }
            return field;
        }

        /// <summary>
        /// ��������_�u���N�H�[�g�ň͂�
        /// </summary>
        private string EncloseDoubleQuotes(string field)
        {
            if (field.IndexOf('"') > -1)
            {
                //"��""�Ƃ���
                field = field.Replace("\"", "\"\"");
            }
            return "\"" + field + "\"";
        }

        /// <summary>
        /// ��������_�u���N�H�[�g�ň͂ޕK�v�����邩���ׂ�
        /// </summary>
        private bool NeedEncloseDoubleQuotes(string field)
        {
            return field.IndexOf('"') > -1 ||
                field.IndexOf(',') > -1 ||
                field.IndexOf('\r') > -1 ||
                field.IndexOf('\n') > -1 ||
                field.StartsWith(" ") ||
                field.StartsWith("\t") ||
                field.EndsWith(" ") ||
                field.EndsWith("\t");
        }

        /// <summary>
        /// �C���|�[�g�{�^���������̃C�x���g
        /// </summary>
        private void import_Click(object sender, EventArgs e)
        {
            // CSV�t�@�C����ǂݎ�鎞�Ɏg��Encoding
            System.Text.Encoding enc = System.Text.Encoding.GetEncoding("Shift_JIS");

            // �ǂݍ��݂���CSV�t�@�C�����_�C�A���O���I�����ĊJ��
            using (StreamReader sr = new StreamReader(DialogOpen(), enc, false))
            {
                // 1�s�ڂł͂Ȃ����ǂ���
                // 1�s�ڂ̓w�b�_�[�ɂȂ��Ă��邽�ߓǂݍ���ł͂����Ȃ�
                bool notFirst = false;
                // �����܂ŌJ��Ԃ�
                while(!sr.EndOfStream)
                {
                    // CSV�t�@�C����1�s��ǂݍ���
                    string line = sr.ReadLine();

                    // 2�s�ڈȍ~�̏ꍇ
                    if (notFirst)
                    {
                        // �ǂݍ���1�s���J���}���ɕ����Ĕz��Ɋi�[����
                        string[] values = line.Split(',');

                        // �z�񂩂烊�X�g�Ɋi�[����
                        List<string> lists = new List<string>();
                        lists.AddRange(values);

                        // ���X�g����DB�ɃC���|�[�g
                        importDB(lists);
                    }
                    notFirst = true;
                }
                // ��ʂ̍X�V
                ScreenDisplay();
            }

        }

        private string DialogOpen()
        {
            // OpenFileDialog�N���X�̃C���X�^���X���쐬
            OpenFileDialog ofd = new OpenFileDialog();

            //�t�@�C���̎�ނɕ\�������I�������w�肷��
            ofd.Filter = "csv�t�@�C��(*.csv)|*.csv";
            // �t�@�C���̎�ނł͂��߂ɑI���������̂��w�肷��
            ofd.FilterIndex = 0;
            // �^�C�g����ݒ肷��
            ofd.Title = "�J���t�@�C����I�����Ă�������";
            // �_�C�A���O�{�b�N�X�����O�Ɍ��݂̃f�B���N�g���𕜌�����悤�ɂ���
            ofd.RestoreDirectory = true;

            // �_�C�A���O��\������
            if (ofd.ShowDialog() == DialogResult.OK)
            {
            }

            return ofd.FileName;
        }

        /// <summary>
        /// �f�[�^�x�[�X�ɃC���|�[�g����ۂ̃N�G���̏���
        /// </summary>
        /// <param name="lists"></param>
        private void importDB(List<string> lists)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // �N�G�����쐬
                    string cmdtxt = "SET IDENTITY_INSERT contacts ON " +
                                    "MERGE INTO contacts AS target " +
                                    "USING " +
                                        "(VALUES (" + lists[0] + ",'" + lists[1] + "','" + lists[2] + "','" + lists[3] + "','" + lists[4] + "')" +
                                        ")AS source(id, name, tel, address, remark) " +
                                    "ON target.id = source.id " +
                                    "WHEN MATCHED THEN " +
                                        "UPDATE SET target.name = source.name, " +
                                        "target.tel = source.tel, " +
                                        "target.address = source.address, " +
                                        "target.remark = source.remark " +
                                    "WHEN NOT MATCHED THEN " +
                                        "INSERT(id, name, tel, address, remark) " +
                                        "VALUES(source.id, source.name, source.tel, source.address, source.remark); " +
                                  "SET IDENTITY_INSERT contacts OFF";

                    // MessageBox.Show(cmdtxt);
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        conn.Open();
                        cmd.CommandText = cmdtxt;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
