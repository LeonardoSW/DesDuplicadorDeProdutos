using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DesDuplicadorHub;
using DesDuplicadorHub.Deletes;


namespace DesDuplicadorHub.Connection
{
    class database
    {
        public string stringConnection= "SERVER=seuServidor; DATABASE=seubanco; UID=root; PWD=suasenha; PORT=3306";
        public List<string> failed = new List<string>();

        public database() { }
        public database(string idTenant, DataGridView dgv) 
        {
            List<int> tenants = getProducts(idTenant, dgv);
            MessageBox.Show("Produtos para exclusão carregados.");
            deleteProducts del = new deleteProducts(tenants);
        }

        public List<int> getProducts(string idTenant, DataGridView dgv)
        {
            string query = $"SELECT distinct pd.idProduct as ID, pd.name as Produto FROM product pd, product pd2 WHERE pd.idProduct > pd2.idProduct AND pd.sourceSku = pd2.sourceSku AND coalesce(pd.destinationSku, '') = coalesce(pd2.destinationSku, '') AND pd.idSystemDestination = pd2.idSystemDestination AND pd.idTenant = pd2.idTenant AND coalesce(pd.idTypeListing, 0) = coalesce(pd2.idTypeListing, 0) AND pd.idTenant = {idTenant}";
            List<int> ids = new List<int>();
            MySqlConnection con = new MySqlConnection(stringConnection);
            MySqlCommand cmd = new MySqlCommand(query, con);
            try
            {   
                con.Open();

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable table = new DataTable();

                adapter.Fill(table);

                dgv.DataSource = table;

                con.Close();

                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.Cells[0].Value != null)
                        ids.Add(int.Parse(row.Cells[0].Value.ToString()));
                }
            }

            catch (Exception e)
            {
                con.Close();
                MessageBox.Show($"Chame o time de Analise Dev\n\n{e.Message}");
                return null;
            }
            return ids;
        }

        public void deleteProductDB(int idProduct)
        {
            string queryDeleteImage = $"DELETE FROM productImage WHERE idProduct = {idProduct}";
            string queryDeleteAction = $"DELETE FROM productActionProduct WHERE idProduct = {idProduct}";
            string queryDeleteAtribute = $"DELETE FROM attribute WHERE idProduct = {idProduct}";
            string queryDeleteCampaingn = $"DELETE FROM priceCampaignProduct WHERE idProduct = {idProduct}";
            string queryDeleteProduct = $"DELETE FROM product WHERE idProduct = {idProduct}";
            MySqlConnection con = new MySqlConnection(stringConnection);
            MySqlCommand cmdImg = new MySqlCommand(queryDeleteImage, con);
            MySqlCommand cmdAction = new MySqlCommand(queryDeleteAction, con);
            MySqlCommand cmdAtributes = new MySqlCommand(queryDeleteAtribute, con);
            MySqlCommand cmdCampaingn = new MySqlCommand(queryDeleteCampaingn, con);
            MySqlCommand cmdProduct = new MySqlCommand(queryDeleteProduct, con);

            try
            {
                con.Open();

                cmdImg.ExecuteNonQuery();
                cmdAction.ExecuteNonQuery();
                cmdAtributes.ExecuteNonQuery();
                cmdCampaingn.ExecuteNonQuery();
                cmdProduct.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception e)
            {
                con.Close();
//                failed.Add($"-Falha na exclusão de 'product' do produto ID: {idProduct}\nErro:{e.Message}");
            }
        }
        //Subistituido para ter fluxo mais leve. :


 /*       public void deleteActionsDB(int idProduct)
        {
            string queryDeleteAction = $"DELETE FROM productActionProduct WHERE idProduct = {idProduct}";
            MySqlConnection con = new MySqlConnection(stringConnection);
            MySqlCommand cmd = new MySqlCommand(queryDeleteAction, con);
            try
            {
                con.Open();

                if (cmd.ExecuteNonQuery() <= 0)
                    failed.Add($"-Falha na exclusão de 'ActionProduct' do produto ID: {idProduct}\nErro: 0 linhas afetadas.");

                con.Close();
            }
            catch (Exception e)
            {
                con.Close();
                failed.Add($"-Falha na exclusão de 'ActionProduct' do produto ID: { idProduct}\nErro:{e.Message}");
            }
        }
 */
 /*
        public void deleteAtributesDB(int idProduct)
        {
            string queryDeleteAtribute = $"DELETE FROM attribute WHERE idProduct = {idProduct}";
            MySqlConnection con = new MySqlConnection(stringConnection);
            MySqlCommand cmd = new MySqlCommand(queryDeleteAtribute, con);
            try
            {
                con.Open();

                if (cmd.ExecuteNonQuery() <= 0)
                    failed.Add($"-Falha na exclusão de 'attribute' do produto ID: {idProduct}\nErro: 0 linhas afetadas.");

                con.Close();
            }
            catch (Exception e)
            {
                con.Close();
                failed.Add($"-Falha na exclusão de 'attribute' do produto ID: { idProduct}\nErro:{e.Message}");
            }
        }
 */
 /*
        public void deleteCampaingnDB(int idProduct)
        {
            string queryDeleteCampaingn = $"DELETE FROM priceCampaignProduct WHERE idProduct = {idProduct}";
            MySqlConnection con = new MySqlConnection(stringConnection);
            MySqlCommand cmd = new MySqlCommand(queryDeleteCampaingn, con);
            try
            {
                con.Open();

                if (cmd.ExecuteNonQuery() <= 0)
                    failed.Add($"-Falha na exclusão de 'priceCampaignProduct' do produto ID: {idProduct}\nErro: 0 linhas afetadas.");

                con.Close();
            }
            catch (Exception e)
            {
                con.Close();
                failed.Add($"-Falha na exclusão de 'priceCampaignProduct' do produto ID: { idProduct}\nErro:{e.Message}");
            }
        }
 */
 /*
        public void deleteProductDB(int idProduct)
        {
            string queryDeleteProduct = $"DELETE FROM product WHERE idProduct = {idProduct}";
            MySqlConnection con = new MySqlConnection(stringConnection);
            MySqlCommand cmd = new MySqlCommand(queryDeleteProduct, con);
            try
            {
                con.Open();

                if (cmd.ExecuteNonQuery() <= 0)
                    failed.Add($"-Falha na exclusão de 'product' do produto ID: {idProduct}\nErro: 0 linhas afetadas.");

                con.Close();
            }
            catch (Exception e)
            {
                con.Close();
                failed.Add($"-Falha na exclusão de 'product' do produto ID: { idProduct}\nErro:{e.Message}");
            }
        }
 */

    }
}
