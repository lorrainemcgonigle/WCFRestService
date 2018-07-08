using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WcfDemoWebApp.ServiceReference1;

namespace WcfDemoWebApp
{
    public partial class _Default : Page
    {
        ServiceReference1.Service1Client proxy;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    proxy = new ServiceReference1.Service1Client();
                    GridViewBookDetails.DataSource = proxy.GetBooks();
                    GridViewBookDetails.DataBind();
                }
                catch (FaultException<ExceptionMessage> exceptionFromService)
                {
                    lblMsg.Text = "Error while loading book details :" + exceptionFromService.Detail.errorMessageOfAction;
                }
                catch (Exception exception)
                {
                    lblMsg.Text = "Error while loading book details :" + exception.Message;
                }
            }
        }
        protected void InsertButton_Click(object sender, EventArgs e)
        {
            try
            {
                int isbn = 0;
                if (txtIsbn.Text != null && txtIsbn.Text != string.Empty)
                {
                    isbn = Convert.ToInt32(txtIsbn.Text);
                }
                string title = txtTitle.Text.Trim();
                string author = txtAuthor.Text.Trim();
                int pages = Convert.ToInt32(txtPages.Text.Trim());
                string publisher = txtPublisher.Text.Trim();

                proxy = new ServiceReference1.Service1Client();
                ServiceReference1.Book newBook =
                new ServiceReference1.Book()
                {
                    isbn = isbn,
                    title = title,
                    author = author,
                    pages = pages,
                    publisher = publisher
                };

                proxy.InsertBook(newBook);

                GridViewBookDetails.DataSource = proxy.GetBooks();
                GridViewBookDetails.DataBind();
                lblMsg.Text = "Record Saved Successfully";
            }
            catch (FaultException<ExceptionMessage> exceptionFromService)
            {
                if (ButtonInsert.Visible == true)
                {
                    lblMsg.Text = "Error while adding new customer details :" + exceptionFromService.Detail.errorMessageOfAction;
                }
                else
                {
                    lblMsg.Text = "Error while updating customer details :" + exceptionFromService.Detail.errorMessageOfAction;
                }
            }
            catch (Exception exception)
            {
                if (ButtonInsert.Visible == true)
                {
                    lblMsg.Text = "Error while adding new customer details :" + exception.Message;
                }
                else
                {
                    lblMsg.Text = "Error while updating customer details :" + exception.Message;
                }
            }

            ResetAll();
        }

        private void ResetAll()
        {
            ButtonInsert.Visible = true;
            ButtonUpdate.Visible = false;
            ButtonDelete.Visible = false;
            ButtonCancel.Visible = false;
            txtIsbn.Text = "";
            txtTitle.Text = "";
            txtAuthor.Text = "";
            txtPages.Text = "";
            txtPublisher.Text = "";
        }
        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                int isbn = Convert.ToInt32(txtIsbn.Text);
                proxy = new ServiceReference1.Service1Client();
                proxy.DeleteBook(isbn);
            }
            catch (FaultException<ExceptionMessage> exceptionFromService)
            {
                lblMsg.Text = "Error while deleteing student details :" + exceptionFromService.Detail.errorMessageOfAction;
            }
            catch (Exception exception)
            {
                lblMsg.Text = "Error while deleteing student details :" + exception.Message;
            }
        }
        protected void CancelButton_Click(object sender, EventArgs e)
        {
            ResetAll();
        }
        protected void GridViewBookDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtIsbn.Text = GridViewBookDetails.DataKeys[GridViewBookDetails.SelectedRow.RowIndex].Value.ToString();
            txtTitle.Text = (GridViewBookDetails.SelectedRow.FindControl("lblTitle") as Label).Text;
            txtAuthor.Text = (GridViewBookDetails.SelectedRow.FindControl("lblAuthor") as Label).Text;
            txtPages.Text = (GridViewBookDetails.SelectedRow.FindControl("lblPages") as Label).Text;
            txtPublisher.Text = (GridViewBookDetails.SelectedRow.FindControl("lblPublisher") as Label).Text;
            //make invisible Insert button during update/delete
            ButtonInsert.Visible = false;
            ButtonUpdate.Visible = true;
            ButtonDelete.Visible = true;
            ButtonCancel.Visible = true;
        }


    }
}