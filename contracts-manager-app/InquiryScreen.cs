using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.InteropServices.Marshalling;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace contracts_manager_app
{
    public partial class InquiryScreen : Form
    {
        public DataTable contacts { get; set; }

        // �f�[�^�x�[�X�Ƃ̐ڑ�������쐬
        static string connectionString = @"Data Source = DSP407\SQLEXPRESS; Initial Catalog = contacts-manager-app; User ID = toru_yoshida; Password = 05211210; Encrypt = False; TrustServerCertificate=true";

        private RegistOrUpdate registOrUpdateScreen;

        public Contact contact1 { get; set; }

        // ���ꂼ��̏�񂪉���ڂɂ��邩
        private int updateIndex;    // �ҏW�{�^��
        private int deleteIndex;    // �폜�{�^��
        private int idIndex;        // id
        private int nameIndex;      // ���O
        private int telIndex;       // �d�b�ԍ�
        private int addressIndex;   // ���[���A�h���X
        private int remarkIndex;    // ���l

        public InquiryScreen()
        {
            InitializeComponent();

            // DataTable�̏�����
            contacts = new DataTable();

            // DataGridView�̏�����
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

            // �{�^���̔w�i�F�ύX
            updateButton.FlatStyle = FlatStyle.Flat;
            deleteButton.FlatStyle = FlatStyle.Flat;
            updateButton.DefaultCellStyle.BackColor = Color.LightGreen;
            deleteButton.DefaultCellStyle.BackColor = Color.Coral;

            // DataGridView�ɒǉ�����
            dataGridView1.Columns.Add(updateButton);
            dataGridView1.Columns.Add(deleteButton);            
            contacts.Columns.Add("id", typeof(int));
            contacts.Columns.Add("name", typeof(string));
            contacts.Columns.Add("tel", typeof(string));
            contacts.Columns.Add("address", typeof(string));
            contacts.Columns.Add("remark", typeof(string));

            // ��񂪂��ꂼ�� dataGridView �̉���ڂɂ��邩
            updateIndex = dataGridView1.Columns["�ҏW"].Index;
            deleteIndex = dataGridView1.Columns["�폜"].Index;
            idIndex = dataGridView1.Columns["id"].Index;
            nameIndex = dataGridView1.Columns["name"].Index;
            telIndex = dataGridView1.Columns["tel"].Index;
            addressIndex = dataGridView1.Columns["address"].Index;
            remarkIndex = dataGridView1.Columns["remark"].Index;

            // dataGridView�̃��C�A�E�g�p
            dataGridView1.Columns[updateIndex].FillWeight = 1.0f;
            dataGridView1.Columns[deleteIndex].FillWeight = 1.0f;
            dataGridView1.Columns[idIndex].FillWeight = 1.0f;
            dataGridView1.Columns[nameIndex].FillWeight = 4.0f;
            dataGridView1.Columns[telIndex].FillWeight = 2.6f;
            dataGridView1.Columns[addressIndex].FillWeight = 5.0f;
            dataGridView1.Columns[remarkIndex].FillWeight = 7.5f;

            // �J���������w��
            dataGridView1.Columns[nameIndex].HeaderText = "���O";
            dataGridView1.Columns[telIndex].HeaderText = "�d�b�ԍ�";
            dataGridView1.Columns[addressIndex].HeaderText = "���[���A�h���X";
            dataGridView1.Columns[remarkIndex].HeaderText = "���l";

            // id�̗���\���ɂ���
            dataGridView1.Columns[idIndex].Visible = false;

            dataGridView1.Columns[updateIndex].HeaderText = "";
            dataGridView1.Columns[deleteIndex].HeaderText = "";

            // �o�^�E�ҏW��ʂ̃C���X�^���X�쐬
            registOrUpdateScreen = new RegistOrUpdate(this);

            contact1 = new Contact("", "", "", "", "");
        }

        /// <summary>
        /// ���[�h���̃C�x���g
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            // DatagridView�̕\��
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

                    // �s�t�B���^�[���I�t�ɂ���
                    contacts.DefaultView.RowFilter = null;

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

            // dataGridView�̏����\���ŃZ����I�������Ȃ�
            dataGridView1.CurrentCell = null;
            dataGridView1.ClearSelection();

        }

        /// <summary>
        /// �V�K�o�^�{�^���������̃C�x���g
        /// </summary>
        private void register_Click(object sender, EventArgs e)
        {
            contact1.id = "0";
            registOrUpdateScreen.update = false;
            // �{�^���̕\����"�o�^"�ɕύX
            registOrUpdateScreen.LabelChanger("�o�^", "�V�K�ǉ����");
            // �o�^��ʂ̕\��
            registOrUpdateScreen.ShowDialogPlus();
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
                UpdateClick(e);
            }

            // "�폜"�{�^�����������Ƃ��̏���
            else if (dgv.Columns[e.ColumnIndex].Name == "�폜")
            {
                DeleteClick(e);                
            }
        }

        /// <summary>
        /// �ҏW�{�^���������̃C�x���g�̏������e
        /// </summary>
        private void UpdateClick(DataGridViewCellEventArgs e)
        {
            // �ҏW�A�X�V��ʂ̃{�^���̕\�L��"�X�V"�ɕύX
            registOrUpdateScreen.LabelChanger("�X�V", "�ҏW���");
            registOrUpdateScreen.update = true;
            // �����ꂽ"�ҏW"�{�^���̍s�̏��擾�A�i�[
            RowInfoStore(e);

            // �J�ڐ�̉�ʂ̃e�L�X�g�{�b�N�X�Ɏ����I�ɓ���
            registOrUpdateScreen.TextBoxRegister(contact1);

            // ��ʑJ��
            registOrUpdateScreen.ShowDialogPlus();
        }

        /// <summary>
        /// �����ꂽ�s�̏��擾�A�i�[�̏������e
        /// </summary>
        private void RowInfoStore(DataGridViewCellEventArgs e)
        {
            // �����ꂽ�s�̏��擾�A�i�[
            contact1.id = dataGridView1.Rows[e.RowIndex].Cells[idIndex].Value.ToString();
            contact1.name = dataGridView1.Rows[e.RowIndex].Cells[nameIndex].Value.ToString();
            contact1.tel = dataGridView1.Rows[e.RowIndex].Cells[telIndex].Value.ToString();
            contact1.address = dataGridView1.Rows[e.RowIndex].Cells[addressIndex].Value.ToString();
            // ���l�͉������͂���Ă��Ȃ��ꍇ�����邽��if���Ŕ��f
            if (dataGridView1.Rows[e.RowIndex].Cells[remarkIndex].Value != null) 
                contact1.remark = dataGridView1.Rows[e.RowIndex].Cells[remarkIndex].Value.ToString();
        }

        /// <summary>
        /// �폜�{�^���������̃C�x���g�̏������e
        /// </summary>
        /// <param name="e"></param>
        private void DeleteClick(DataGridViewCellEventArgs e)
        {
            // �폜���邩�̊m�F���b�Z�[�W�{�b�N�X��\��
            DialogResult result = MessageBox.Show("�A������폜���܂����H", "�m�F",
                   MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);

            // OK���������Ƃ��̏���
            if (result == DialogResult.OK)
            {
               PushDeleteOk(e);
            }
        }


        /// <summary>
        /// �폜�����o�����Ƃ��̏���
        /// </summary>
        private void PushDeleteOk(DataGridViewCellEventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // �폜�������s��id���擾
                    contact1.id = dataGridView1.Rows[e.RowIndex].Cells[idIndex].Value.ToString();
                    // �N�G�����쐬
                    string cmdtest = "DELETE FROM contacts WHERE id = " + contact1.id;

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
            // ��ʂ̍ĕ\��
            ScreenDisplay();
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
                contacts.DefaultView.RowFilter = @$"name LIKE '%{searchBox.Text}%'
                                                    OR remark LIKE'%{searchBox.Text}%' ";
            }
        }

        /// <summary>
        /// �G�N�X�|�[�g�{�^���������̃C�x���g
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void export_Click(object sender, EventArgs e)
        {
            // �G�N�X�|�[�g����f�[�^�����݂��邩�ǂ���
            if (contacts.Rows.Count > 0)
            {
                // �G�N�X�|�[�g����f�[�^���ݎ��̏���
                ExportEvent exportEvent = new ExportEvent(contacts);
                exportEvent.ExportEventOccur();
            }
            else
            {
                MessageBox.Show("�G�N�X�|�[�g����f�[�^�����݂��܂���");
            }
        }

        /// <summary>
        /// �C���|�[�g�{�^���������̃C�x���g
        /// </summary>
        private void import_Click(object sender, EventArgs e)
        {
            ImportEvent importEvent = new ImportEvent(this, connectionString);
            importEvent.ImportEventOccur();
        }

        /// <summary>
        /// �S�A����\���{�^���������̃C�x���g
        /// </summary>
        private void showAllContacts_Click(object sender, EventArgs e)
        {
            ScreenDisplay();
        }
    }
}
