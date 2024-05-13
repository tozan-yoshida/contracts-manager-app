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

        // �o�^�E�X�V��ʂ̃t�H�[��
        private RegistOrUpdate registOrUpdateScreen;

        // �A����̊i�[�N���X
        public Contact contact1 { get; set; }

        // ���ꂼ��̏�񂪉���ڂɂ��邩
        private int updateIndex;    // �ҏW�{�^��
        private int deleteIndex;    // �폜�{�^��
        private int idIndex;        // id
        private int nameIndex;      // ���O
        private int telIndex;       // �d�b�ԍ�
        private int addressIndex;   // ���[���A�h���X
        private int remarkIndex;    // ���l

        // �{�^����
        private DataGridViewButtonColumn updateButtonColumn;
        private DataGridViewButtonColumn deleteButtonColumn;


        public DatabaseHandler databaseHandler { get; set; }

        public InquiryScreen()
        {
            InitializeComponent();

            // DataTable�̏�����
            contacts = new DataTable();

            // DataGridView�̏�����
            dataGridView1.DataSource = contacts;

            // DataGridViewButtonColumn�̍쐬
            updateButtonColumn = new DataGridViewButtonColumn();
            deleteButtonColumn = new DataGridViewButtonColumn();

            // ��̖��O��ݒ�
            updateButtonColumn.Name = "�ҏW";
            deleteButtonColumn.Name = "�폜";

            // ���ׂẴ{�^����"�ҏW"�A"�폜"�ƕ\������
            updateButtonColumn.UseColumnTextForButtonValue = true;
            deleteButtonColumn.UseColumnTextForButtonValue = true;
            updateButtonColumn.Text = "�ҏW";
            deleteButtonColumn.Text = "�폜";

            // �{�^���̔w�i�F�ύX
            updateButtonColumn.FlatStyle = FlatStyle.Flat;
            deleteButtonColumn.FlatStyle = FlatStyle.Flat;
            updateButtonColumn.DefaultCellStyle.BackColor = Color.LightGreen;
            deleteButtonColumn.DefaultCellStyle.BackColor = Color.Coral;

            // DataGridView�ɒǉ�����
            dataGridView1.Columns.Add(updateButtonColumn);
            dataGridView1.Columns.Add(deleteButtonColumn);
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
            dataGridView1.Columns[telIndex].FillWeight = 2.8f;
            dataGridView1.Columns[addressIndex].FillWeight = 5.0f;
            dataGridView1.Columns[remarkIndex].FillWeight = 7.5f;

            // �J���������w��
            dataGridView1.Columns[updateIndex].HeaderText = "";
            dataGridView1.Columns[deleteIndex].HeaderText = "";
            dataGridView1.Columns[nameIndex].HeaderText = "���O";
            dataGridView1.Columns[telIndex].HeaderText = "�d�b�ԍ�";
            dataGridView1.Columns[addressIndex].HeaderText = "���[���A�h���X";
            dataGridView1.Columns[remarkIndex].HeaderText = "���l";

            // id�̗���\���ɂ���
            dataGridView1.Columns[idIndex].Visible = false;

            // �f�[�^�x�[�X�n���h���̃C���X�^���X�쐬
            databaseHandler = new DatabaseHandler(connectionString);

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
        /// DataGridView�̕\��DB���(��)�擾����
        /// </summary>
        public void ScreenDisplay()
        {
            // �\���̏�����
            contacts.Clear();

            // �s�t�B���^�[���I�t�ɂ���
            contacts.DefaultView.RowFilter = null;

            // DB�ォ��f�[�^�e�[�u���Ƀf�[�^��n��
            databaseHandler.DataAdaptDataTable("SELECT * FROM contacts", contacts);

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
            // �폜�������s��id���擾
            contact1.id = dataGridView1.Rows[e.RowIndex].Cells[idIndex].Value.ToString();

            // Delete�N�G�����쐬�A���s
            databaseHandler.DeleteContact(contact1.id);

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
                // �����̓e�L�X�g�{�b�N�X�̕����񂪖��O�A���l�̂����ꂩ�̈ꕔ�������͑S���Ɉ�v
                contacts.DefaultView.RowFilter = @$"name LIKE '%{searchBox.Text}%'
                                                    OR remark LIKE'%{searchBox.Text}%' ";
            }
            // dataGridView�̏����\���ŃZ����I�������Ȃ�
            dataGridView1.CurrentCell = null;
            dataGridView1.ClearSelection();
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
                ExportEvent();
            }
            else
            {
                MessageBox.Show("�G�N�X�|�[�g����f�[�^�����݂��܂���");
            }
        }

        /// <summary>
        /// �G�N�X�|�[�g����f�[�^���ݎ��̏���
        /// </summary>
        private void ExportEvent()
        {
            // csv�t�@�C���̃p�X���t�H���_���w�肵�Ď擾
            FileDialogUse fileDialogUse = new FileDialogUse(new SaveFileDialog());

            // CSV�t�@�C���ɏ������ނƂ��Ɏg��Encoding
            System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("Shift_JIS");

            // �������ރt�H���_�̕ۑ���Ɩ��O���w�肷��
            // �w�肵��OK�{�^�����������ꍇ�A�ȉ��̏������s��
            if (fileDialogUse.DialogUse())
            {
                ExportPushOK(fileDialogUse.fileDialog.FileName, encoding);
                MessageBox.Show($@"{fileDialogUse.fileDialog.FileName}�ɃG�N�X�|�[�g���܂���");
            }
        }

        /// <summary>
        /// �_�C�A���O��OK�������̏���
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="encoding"></param>
        private void ExportPushOK(string fileName, System.Text.Encoding encoding)
        {
            try
            {
                using (StreamWriter sr = new StreamWriter(fileName, false, encoding))
                {
                    int colCount = contacts.Columns.Count;  // ��̐�
                    int lastColIndex = colCount - 1;        // �Ō�̗�̗�ԍ�

                    // �w�b�_����������
                    WriteHeader(sr, colCount, lastColIndex);
                    // ���s����
                    sr.Write("\r\n");
                    // ���R�[�h����������
                    WriteRecord(sr, colCount, lastColIndex);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// �w�b�_�̏������ݏ���
        /// </summary>
        /// <param name="colCount">��̐�</param>
        /// <param name="lastColIndex">�Ō�̗�̗�ԍ�</param>
        private void WriteHeader(StreamWriter sr, int colCount, int lastColIndex)
        {
            // �񐔂����J��Ԃ�
            for (int i = 0; i < colCount; i++)
            {
                // �w�b�_�̎擾
                string field = contacts.Columns[i].Caption;
                // csv�ւ̏������ݏ���
                WriteCsv(sr, lastColIndex, i, field);
            }
        }

        /// <summary>
        /// ���R�[�h�̏������ݏ���
        /// </summary>
        /// <param name="colCount">��̐�</param>
        /// <param name="lastColIndex">�Ō�̗�̗�ԍ�</param>
        private void WriteRecord(StreamWriter sr, int colCount, int lastColIndex)
        {
            foreach (DataRow row in contacts.Rows)
            {
                for (int i = 0; i < colCount; i++)
                {
                    // �t�B�[���h�̎擾
                    string field = row[i].ToString();
                    // csv�ւ̏������ݏ���
                    WriteCsv(sr, lastColIndex, i, field);
                }
                // ���s����
                sr.Write("\r\n");
            }
        }

        /// <summary>
        /// csv�t�@�C���ւ̏������ݏ���
        /// </summary>
        /// <param name="lastColIndex">�Ō�̗�̗�ԍ�</param>
        /// <param name="field">csv�ɏ������ތ��f�[�^</param>
        private void WriteCsv(StreamWriter sr, int lastColIndex, int i, string field)
        {
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

        /// <summary>
        /// ��������_�u���N�H�[�g�ň͂�
        /// </summary>
        private String EncloseDoubleQuotesIfNeed(string field)
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
            System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("Shift_JIS");
            // �t�@�C���_�C�A���O���g�p����ۂ̃C���X�^���X
            FileDialogUse fileDialogUse = new FileDialogUse(new OpenFileDialog());

            // �_�C�A���O��\���AOK�{�^���������ꂽ�Ȃ�C���|�[�g�̏������s��
            if (fileDialogUse.DialogUse())
            {
                ImportPushOK(fileDialogUse.fileDialog.FileName, encoding);
            }
        }

        /// <summary>
        /// �_�C�A���O��OK�������̏���
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="encoding"></param>
        private void ImportPushOK(string fileName, System.Text.Encoding encoding)
        {
            try
            {
                // �ǂݍ��݂���CSV�t�@�C�����_�C�A���O���I�����ĊJ��
                using (StreamReader sr = new StreamReader(fileName, encoding, false))
                {
                    // 1�s�ڂł͂Ȃ����ǂ���
                    // 1�s�ڂ̓w�b�_�[�ɂȂ��Ă��邽�ߏ����o���Ă͂����Ȃ�
                    bool notFirst = false;
                    // �����܂ŌJ��Ԃ�
                    ReadCsv(sr, notFirst);
                    // ��ʂ̍X�V
                    ScreenDisplay();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("�G���[: " + ex.Message);
            }
        }

        /// <summary>
        /// csv�t�@�C���̓ǂݍ��݂̌J��Ԃ�����
        /// </summary>
        /// <param name="sr"></param>
        /// <param name="notFirst">�ǂݍ��񂾍s��1�s�ڂ��ۂ�</param>
        private void ReadCsv(StreamReader sr, bool notFirst)
        {
            while (!sr.EndOfStream)
            {
                // CSV�t�@�C����1�s�ǂݍ���
                string line = sr.ReadLine();

                // 2�s�ڈȍ~�̏ꍇ
                if (notFirst)
                {
                    ReadCsvNotFirst(line);
                }
                notFirst = true;
            }
        }

        /// <summary>
        /// csv�t�@�C����2�s�ڈȍ~��ǂݍ��񂾎��̏���
        /// </summary>
        /// <param name="line"></param>
        private void ReadCsvNotFirst(string line)
        {
            // �ǂݍ���1�s���J���}���ɕ����Ĕz��Ɋi�[����
            string[] values = line.Split(',');

            // �z�񂩂�R���^�N�g�N���X�Ɋi�[����
            Contact contact = new Contact(values[0], values[1], values[2], values[3], values[4]);
            // �R���^�N�g�N���X��DB�ɃC���|�[�g
            databaseHandler.MergeIntoContact(contact);
        }

        /// <summary>
        /// �ēǍ��{�^���������̃C�x���g
        /// </summary>
        private void showAllContacts_Click(object sender, EventArgs e)
        {
            ScreenDisplay();
            searchBox.Text = "";
        }
    }
}
