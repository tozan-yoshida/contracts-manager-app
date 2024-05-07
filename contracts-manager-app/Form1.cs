using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq.Expressions;

namespace contracts_manager_app
{
    public partial class Form1 : Form
    {
        public DataTable contacts;
        // �f�[�^�x�[�X�Ƃ̐ڑ�������쐬
        static string connectionString = @"Data Source = DSP407\SQLEXPRESS; Initial Catalog = contacts-manager-app; User ID = toru_yoshida; Password = 05211210; Encrypt = False; TrustServerCertificate=true";

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
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // DataGridView�̏�����
            // �J���������w��
            dataGridView1.DataSource = contacts;

            // DataGridViewButtonColumn�̍쐬
            DataGridViewButtonColumn update = new DataGridViewButtonColumn();
            DataGridViewButtonColumn delete = new DataGridViewButtonColumn();

            // ��̖��O��ݒ�
            update.Name = "�ҏW";
            delete.Name = "�폜";

            // ���ׂẴ{�^����"�ҏW"�A"�폜"�ƕ\������
            update.UseColumnTextForButtonValue = true;
            delete.UseColumnTextForButtonValue = true;
            update.Text = "�ҏW";
            delete.Text = "�폜";

            // DataGridView�ɒǉ�����
            dataGridView1.Columns.Add(update);
            dataGridView1.Columns.Add(delete);

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
        private void ScreenDisplay()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

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

        private void register_Click(object sender, EventArgs e)
        {

        }
    }
}
