using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using ProjectManagement.Models;
using System.IO;

namespace ProjectManagement.Controllers
{
    public class HomeController : Controller
    {
        static String cs = "Data Source=SWARNA-LT\\SQLEXPRESS;Initial Catalog=Projects;Integrated Security=True";
        SqlConnection con = new SqlConnection(cs);
        static List<Company> list = new List<Company>();
        Company c=new Company();
        static List<Client> clientList = new List<Client>();
        static List<Project> projectList = new List<Project>();
        Client client = new Client();
        Project project = new Project();
        byte[] bytes=null;
        static string companyId = null;
        static string clientId = null;
        string []status={"Analysis","Planning","Construction","Testing","Deployed","Completed"};
        //
        // GET: /Home/
        public ActionResult DashBoard()
        {
            return View();
        }
        
        public ActionResult Companies()
        {
            list = new List<Company>();
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Company", con);
            SqlDataReader data = cmd.ExecuteReader();
            while (data.Read())
            {
                if (Convert.ToInt32(data["active"]) == 1)
                {
                    System.Diagnostics.Debug.Write("qqqqqqqqqqqqqqqqqqqqqq");
                    c = new Company();
                    c.ceoName = data["ceoName"].ToString();
                    c.companyName = data["companyName"].ToString();
                    c.email = data["email"].ToString();
                    c.headquarters = data["headquarters"].ToString();
                    c.id = Convert.ToInt32(data["id"]);
                    c.pendingProjects = Convert.ToInt32(data["pendingProject"]);
                    c.totalProjects = Convert.ToInt32(data["totalProject"]);
                    c.phoneNumber = Convert.ToInt32(data["phoneNumber"]);
                    System.Diagnostics.Debug.WriteLine(c.id);
                    list.Add(c);
                }
            }
            con.Close();
            return View(list);
        }
        [HttpGet]
        public ActionResult ActionOnCompany(string act,int id)
        {
            switch(act)
            {
                case "View":
                    return RedirectToAction("View",id);
                case "Clients":
                    return RedirectToAction("Clients",id);

            }
            return RedirectToAction("Companies");
        }
        public ActionResult View(int id)
        {
            c = new Company();
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Company where id=@id",con);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader data = cmd.ExecuteReader();
            while (data.Read())
            {
                System.Diagnostics.Debug.Write("qqqqqqqqqqqqqqqqqqqqqq");
                c = new Company();
                c.ceoName = data["ceoName"].ToString();
                c.companyName = data["companyName"].ToString();
                c.email = data["email"].ToString();
                c.headquarters = data["headquarters"].ToString();
                c.companyType = data["companyType"].ToString();
                c.id = Convert.ToInt32(data["id"]);
                c.pendingProjects = Convert.ToInt32(data["pendingProject"]);
                c.totalProjects = Convert.ToInt32(data["totalProject"]);
                c.phoneNumber = Convert.ToInt32(data["phoneNumber"]);
                c.image = (byte[])data["image"];
                System.Diagnostics.Debug.WriteLine(c.id);
                list.Add(c);
            }
            con.Close();
            return View(c);
        }
        public ActionResult Edit(int id)
        {
            Company c = new Company();
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Company where id=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader data = cmd.ExecuteReader();
            while (data.Read())
            {
                c = new Company();
                c.ceoName = data["ceoName"].ToString();
                c.companyName = data["companyName"].ToString();
                c.email = data["email"].ToString();
                c.headquarters = data["headquarters"].ToString();
                c.companyType = data["companyType"].ToString();
                c.id = Convert.ToInt32(data["id"]);
                c.pendingProjects = Convert.ToInt32(data["pendingProject"]);
                c.totalProjects = Convert.ToInt32(data["totalProject"]);
                c.phoneNumber = Convert.ToInt32(data["phoneNumber"]);
                //c.image = (byte[])data["image"];
            }
            return View(c);
        }
        [HttpPost]
        public ActionResult Update(Models.Company c)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("update Company set companyName=@companyName,ceoName=@ceoName,totalProject=@totalProject,pendingProject=@pendingProject,email=@email,headquarters=@headquarters,phoneNumber=@phoneNumber,companyType=@companyType where id=@id", con);
            cmd.Parameters.AddWithValue("@id", c.id);
            cmd.Parameters.AddWithValue("@companyName", c.companyName);
            cmd.Parameters.AddWithValue("@ceoName", c.ceoName);
            cmd.Parameters.AddWithValue("@totalProject", c.totalProjects);
            cmd.Parameters.AddWithValue("@pendingProject", c.pendingProjects);
            cmd.Parameters.AddWithValue("@email", c.email);
            cmd.Parameters.AddWithValue("@headQuarters", c.headquarters);
            cmd.Parameters.AddWithValue("@phoneNumber", c.phoneNumber);
            cmd.Parameters.AddWithValue("@companyType", c.companyType);
            //cmd.Parameters.AddWithValue("@image", bytes);
            cmd.ExecuteNonQuery();
            return RedirectToAction("Companies");
        }
        public ActionResult Delete(int id)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("update Company set active=0 where id=@id",con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            return RedirectToAction("Companies");
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(HttpPostedFileBase filee,Models.Company c)
        {
            if (filee != null)
            {
                using (BinaryReader br = new BinaryReader(filee.InputStream))
                {
                    bytes = br.ReadBytes(filee.ContentLength);
                }
            }
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into Company(id,companyName,ceoName,totalProject,pendingProject,email,headquarters,phoneNumber,companyType,image,active) values(@id,@companyName,@ceoName,@totalProject,@pendingProject,@email,@headquarters,@phoneNumber,@companyType,@image,@active)",con);
            cmd.Parameters.AddWithValue("@id", c.id);
            cmd.Parameters.AddWithValue("@companyName", c.companyName);
            cmd.Parameters.AddWithValue("@ceoName", c.ceoName);
            cmd.Parameters.AddWithValue("@totalProject", c.totalProjects);
            cmd.Parameters.AddWithValue("@pendingProject", c.pendingProjects);
            cmd.Parameters.AddWithValue("@email", c.email);
            cmd.Parameters.AddWithValue("@headQuarters", c.headquarters);
            cmd.Parameters.AddWithValue("@phoneNumber", c.phoneNumber);
            cmd.Parameters.AddWithValue("@companyType", c.companyType);
            cmd.Parameters.AddWithValue("@image", bytes);
            cmd.Parameters.AddWithValue("@active", 1);
            cmd.ExecuteNonQuery();
            return RedirectToAction("Companies");
        }
        [HttpGet]
        public ActionResult Clients()
        {
            System.Diagnostics.Debug.WriteLine("qqqqqqqqqqqqqqqqqqqqqqqqqqq" + companyId);
            string command=null;
            Class1 clas=new Class1();
            //int id=Convert.ToInt32(companyid);
            if(companyId != null)
            command="select * from Client where companyId=@id";
            else
            command="select * from Client";
                clientList = new List<Client>();
                con.Open();
                SqlCommand comd = new SqlCommand(command, con);
                comd.Parameters.AddWithValue("@id", Convert.ToInt32(companyId));
                SqlDataReader clientdata = comd.ExecuteReader();
                while(clientdata.Read())
                {
                    client = new Client();
                    client.clientName = clientdata["clientName"].ToString();
                    client.email = clientdata["email"].ToString();
                    client.clientId = Convert.ToInt32(clientdata["id"]);
                    client.pendingProject = Convert.ToInt32(clientdata["pendingProject"]);
                    client.totalProject = Convert.ToInt32(clientdata["totalProject"]);
                    client.phoneNumber = Convert.ToInt32(clientdata["phoneNumber"]);
                    client.submissionDate = Convert.ToDateTime(clientdata["submissionDate"]);
                    client.partnerCompanyName = clientdata["companyName"].ToString();
                    client.partnerCompanyId = Convert.ToInt32(clientdata["companyId"]);
                    clientList.Add(client);
                }
                companyId = null;
                clas.client = clientList;
                con.Close();
            //List<SelectListItem> li=new List<SelectListItem>()
            List<Company> com = new List<Company>();
            Company c = new Company();            
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Company",con);
            SqlDataReader data = cmd.ExecuteReader();
            while(data.Read())
            {
                c = new Company();
                c.id = Convert.ToInt32(data["id"]);
                c.companyName = data["companyName"].ToString();
                com.Add(c);
            }
            con.Close();
            clas.company = com;
            return View(clas);
        }
        [HttpPost]
        public ActionResult ListClients(string foo,string searchbox)
        {
            if (searchbox == "Search")
            {
                // getting company list for knowing selected value in drop down list
                SelectListItem c = new SelectListItem();
                List<SelectListItem> customerList = new List<SelectListItem>();
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from Company", con);
                SqlDataReader data = cmd.ExecuteReader();
                while (data.Read())
                {
                    c = new SelectListItem();
                    c.Value = (data["id"]).ToString();
                    c.Text = data["companyName"].ToString();
                    customerList.Add(c);
                }
                con.Close();
                if (!string.IsNullOrEmpty(foo))
                {
                    SelectListItem selectedItem = customerList.Find(p => p.Value == foo);
                    companyId = selectedItem.Value;
                }
                return RedirectToAction("Clients");
            }
            else
                return RedirectToAction("Clients");
        }

        public ActionResult AddClient()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddClient(Models.Client client)
        {
            con.Open();
            SqlCommand cmd= new SqlCommand("insert into Client(id,clientName,totalProject,pendingProject,submissionDate,companyId,phoneNumber,email) values(@id,@clientName,@totalProject,@pendingProject,@submissionDate,@companyId,@phoneNumber,@email)",con);
            cmd.Parameters.AddWithValue("@id", client.clientId);
            cmd.Parameters.AddWithValue("@clientName", client.clientName);
            cmd.Parameters.AddWithValue("@totalProject", client.totalProject);
            cmd.Parameters.AddWithValue("@pendingProject", client.pendingProject);
            cmd.Parameters.AddWithValue("@submissionDate", client.submissionDate);        
            cmd.Parameters.AddWithValue("@email", client.email);
            cmd.Parameters.AddWithValue("@phoneNumber", client.phoneNumber);
            cmd.Parameters.AddWithValue("@companyId", client.partnerCompanyId);
            cmd.ExecuteNonQuery();
            return RedirectToAction("Clients");
        }
        public ActionResult Projects()
        {
            System.Diagnostics.Debug.WriteLine("qqqqqqqqqqqqqqqqqqqqqqqqqqq" + clientId);
            string command = null;
            ClientProject clas = new ClientProject();
            //int id=Convert.ToInt32(companyid);
            if (clientId != null)
                command = "select * from Project where clientCompanyId=@id";
            else
                command = "select * from Project";
            projectList = new List<Project>();
            con.Open();
            SqlCommand comd = new SqlCommand(command, con);
            comd.Parameters.AddWithValue("@id", Convert.ToInt32(clientId));
            SqlDataReader projectdata = comd.ExecuteReader();
            while (projectdata.Read())
            {
                project = new Project();
                project.projectName = projectdata["projectName"].ToString();
                project.projectId = Convert.ToInt32(projectdata["projectId"]);               
                project.deadline = Convert.ToDateTime(projectdata["deadline"]);
                //int x= Convert.ToInt32(projectdata["status"]);
                project.status = projectdata["status"].ToString();
                project.clientCompanyId = Convert.ToInt32(projectdata["clientCompanyId"]);
                project.budget = Convert.ToInt32(projectdata["budget"]);
                projectList.Add(project);
            }
            clientId = null;
            clas.project = projectList;
            con.Close();
            //List<SelectListItem> li=new List<SelectListItem>()
            List<Client> com = new List<Client>();
            Client c = new Client();
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Client", con);
            SqlDataReader data = cmd.ExecuteReader();
            while (data.Read())
            {
                c = new Client();
                c.clientId = Convert.ToInt32(data["id"]);
                c.clientName = data["clientName"].ToString();
                com.Add(c);
            }
            con.Close();
            clas.client = com;
            return View(clas);
        }
        [HttpPost]
        public ActionResult ListProjects(string foo,string searchbox)
        {
            if (searchbox == "Search")
            {
                // getting company list for knowing selected value in drop down list                
                SelectListItem c = new SelectListItem();
                List<SelectListItem> customerList = new List<SelectListItem>();
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from Client", con);
                SqlDataReader data = cmd.ExecuteReader();
                while (data.Read())
                {
                    c = new SelectListItem();
                    c.Value = (data["id"]).ToString();
                    c.Text = data["clientName"].ToString();
                    customerList.Add(c);
                }
                con.Close();
                if (!string.IsNullOrEmpty(foo))
                {
                    SelectListItem selectedItem = customerList.Find(p => p.Value == foo);
                    clientId = selectedItem.Value;
                    System.Diagnostics.Debug.WriteLine("qqqqqqqqqqqqqqqqqqqqqqqqqqq" + clientId);
                }
                return RedirectToAction("Projects");
            }
            else
                return RedirectToAction("Projects");
        }
        public ActionResult AddProject()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddProject(Models.Project project)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into Project(projectId,projectName,clientCompanyId,status,deadline,budget) values(@projectId,@projectName,@clientCompanyId,@status,@deadline,@budget)", con);
            cmd.Parameters.AddWithValue("@projectId", project.projectId);
            cmd.Parameters.AddWithValue("@projectName", project.projectName);
            cmd.Parameters.AddWithValue("@clientCompanyId", project.clientCompanyId);
            cmd.Parameters.AddWithValue("@status", status[0]);
            cmd.Parameters.AddWithValue("@deadline", project.deadline);
            cmd.Parameters.AddWithValue("@budget", project.budget);            
            cmd.ExecuteNonQuery();
            return RedirectToAction("Projects");
        }
        public ActionResult Update(int id)
        {
            string stat=null;
            int x=0;
            con.Open();
            SqlCommand cmd = new SqlCommand("select status from Project where projectId=@id",con);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader data=cmd.ExecuteReader();
            
            while(data.Read())
            {
                stat = data["status"].ToString();
            }
            for (int count = 0; count < 6; count++)
                if (stat == status[count])
                    x = count + 1;
            con.Close();
            con.Open();
            SqlCommand comd = new SqlCommand("update Project set status=@status where projectId=@id", con);
            comd.Parameters.AddWithValue("@id", id);
            if(x != 6)
                comd.Parameters.AddWithValue("@status", status[x + 1]);
            else
                comd.Parameters.AddWithValue("@status", status[5]);
            comd.ExecuteNonQuery();
            return RedirectToAction("Projects");
        }
    }
}
