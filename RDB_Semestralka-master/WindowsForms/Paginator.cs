using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsForms
{
    class Paginator
    {
        private Database context;

        private static int nTotalRows;  //total rows for next button
        private static int pTotalRows; //total rows for previos button
        private static int nSkippedRows;  //the steps for skkiping rows in next button
        private static int Total;             //total rows of table
        private const int RowsPerPage = 50;

        public Paginator()
        {
            context = new Database();
            Total = context.countRows();
            pTotalRows = Total;
            nTotalRows = Total;
        }

        public object firstPage()
        {
            object data = null;
            if (nTotalRows <= RowsPerPage)
            {
                data = context.selectAll();
            }
            else if (nTotalRows > RowsPerPage)
            {
                nSkippedRows = RowsPerPage;
                data = context.selectSpecific(0, nSkippedRows);
                //calculate the remaining rows for next button
                nTotalRows = nTotalRows - nSkippedRows;
            }
            return data;
        }

        public object nextPage()
        {
            object data = null;
            if (nTotalRows <= RowsPerPage)
            {
                data = context.selectSpecific(nSkippedRows, nTotalRows);
                pTotalRows -= RowsPerPage;
                nSkippedRows += RowsPerPage;
                nTotalRows -= RowsPerPage;
                //btnNext.Enabled = false;
                //btnPrev.Enabled = true;
            }
            else if (nTotalRows > RowsPerPage)
            {
                data = context.selectSpecific(nSkippedRows, RowsPerPage);
                pTotalRows -= RowsPerPage;
                nTotalRows = nTotalRows - RowsPerPage;
                nSkippedRows += RowsPerPage;
                //btnPrev.Enabled = true;
            }
            return data;
        }

        public object previousPage()
        {
            object data = null;
            if (((Total - pTotalRows) - RowsPerPage == 0) || Total - pTotalRows == 0)
            {
                data = context.selectSpecific(0, RowsPerPage);
                nTotalRows += RowsPerPage;
                nSkippedRows -= RowsPerPage;

                if (nSkippedRows == 0)
                {
                    nSkippedRows = RowsPerPage;
                }

                //calculate total rows for previous button
                pTotalRows += RowsPerPage;
                //btnPrev.Enabled = false;
                //btnNext.Enabled = true;
            }
            else if (Total - pTotalRows > RowsPerPage)
            {
                data = context.selectSpecific(Total - pTotalRows - RowsPerPage, RowsPerPage);
                //btnNext.Enabled = true;
                pTotalRows += RowsPerPage;
                nTotalRows = nTotalRows + RowsPerPage;
                nSkippedRows -= RowsPerPage;
            }
            return data;
        }

    }
}