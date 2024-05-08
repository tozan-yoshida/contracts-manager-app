using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.InteropServices.Marshalling;

namespace contracts_manager_app
{
    public partial class Form1 : Form
    {
        public DataTable contacts { get; set; }
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
        /// �V�K�o�^�{�^�������������̏���
        /// </summary>
        private void register_Click(object sender, EventArgs e)
        {
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
            else if(dgv.Columns[e.ColumnIndex].Name == "�폜")
            {
                DialogResult result = MessageBox.Show("�A������폜���܂����H", "����", 
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);

                // OK���������Ƃ��̏���
                if (result == DialogResult.OK)
                {
                    try
                    {
                        using(SqlConnection conn = new SqlConnection(connectionString))
                        {
                            // �폜�������s��id���擾
                            id = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                            // �N�G�����쐬
                            string cmdtest = "DELETE FROM contacts WHERE id = " + id;

                            using (var cmd =  new SqlCommand(cmdtest, conn)) {
                                // db�ڑ�
                                conn.Open();
                                // �N�G�������s
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    catch(Exception ex)
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
    }
}
