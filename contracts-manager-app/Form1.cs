namespace contracts_manager_app
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // �J���������w��
            dataGridView1.ColumnCount = 4;

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
            dataGridView1.Columns[0].HeaderText = "���O";
            dataGridView1.Columns[1].HeaderText = "�d�b�ԍ�";
            dataGridView1.Columns[2].HeaderText = "���[���A�h���X";
            dataGridView1.Columns[3].HeaderText = "���l";

            // �f�[�^�̒ǉ��e�X�g
            dataGridView1.Rows.Add("���O", "�d�b�ԍ�", "���[���A�h���X", "���l");

        }

        /// <summary>
        /// ��ʂ��i�āj�\������
        /// </summary>
        private void ScreenDisplay()
        {
           

        }
    }
}
