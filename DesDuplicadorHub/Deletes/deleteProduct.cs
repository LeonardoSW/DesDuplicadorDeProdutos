using Aardvark.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DesDuplicadorHub.Connection;
using System.Windows.Forms;

namespace DesDuplicadorHub.Deletes
{
    class deleteProducts
    {

        database db = new database();

        public deleteProducts(List<int> ids)
        {
            
            foreach (int item in ids)
            {
                db.deleteProductDB(item);

                System.Threading.Thread.Sleep(80);
            }

            MessageBox.Show("Processo Finalizado.");
        }
    }
        /*
        public void deleteProduct(int idProduct)
        {
            
        }
        */
}

