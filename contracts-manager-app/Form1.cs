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
            // カラム数を指定
            dataGridView1.ColumnCount = 4;

            // DataGridViewButtonColumnの作成
            DataGridViewButtonColumn update = new DataGridViewButtonColumn();
            DataGridViewButtonColumn delete = new DataGridViewButtonColumn();

            // 列の名前を設定
            update.Name = "編集";
            delete.Name = "削除";

            // すべてのボタンに"編集"、"削除"と表示する
            update.UseColumnTextForButtonValue = true;
            delete.UseColumnTextForButtonValue = true;
            update.Text = "編集";
            delete.Text = "削除";

            // DataGridViewに追加する
            dataGridView1.Columns.Add(update);
            dataGridView1.Columns.Add(delete);

            // カラム名を指定
            dataGridView1.Columns[0].HeaderText = "名前";
            dataGridView1.Columns[1].HeaderText = "電話番号";
            dataGridView1.Columns[2].HeaderText = "メールアドレス";
            dataGridView1.Columns[3].HeaderText = "備考";

            // データの追加テスト
            dataGridView1.Rows.Add("名前", "電話番号", "メールアドレス", "備考");

        }

        /// <summary>
        /// 画面を（再）表示する
        /// </summary>
        private void ScreenDisplay()
        {
           

        }
    }
}
