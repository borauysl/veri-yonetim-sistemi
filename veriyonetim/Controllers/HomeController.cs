using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace veriyonetim.Controllers
{
    public class HomeController : Controller
    {
        // kullanıcı bilgilerini depolamak için oturum anahtarları
        private const string SessionUserIDKey = "UserID";
        private const string SessionUserNameKey = "UserName";

        string connectionString = "Server=localhost;Database=veriyonetimsistemi;Uid=root;Pwd=1234;";
        private MySqlConnection GetConnection()
        {

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            return new MySqlConnection(connectionString);
        }

        //App_Start dosyasında bulunan RouteConfigde homeyi açılacak html sayfası ile aynı yapman gerekiyo eğer 404 alırsan aklında  bulunsun :)

        public ActionResult Giris()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Giris(string userName, string password)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);

            try
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT userID, userName, userRole FROM user WHERE userName = @UserName AND userPassword = @Password";
                cmd.Parameters.AddWithValue("@UserName", userName);
                cmd.Parameters.AddWithValue("@Password", password);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int userID = Convert.ToInt32(reader["userID"]);
                        string retrievedUserName = reader["userName"].ToString();
                        string userRole = reader["userRole"].ToString();

                        System.Web.Security.FormsAuthentication.SetAuthCookie(retrievedUserName, false);

                        // oturum oluştur
                        Session[SessionUserIDKey] = userID;
                        Session[SessionUserNameKey] = retrievedUserName;

                        string redirectAction;
                        switch (userRole)
                        {
                            case "admin":
                                redirectAction = "yoneticiTabloGoruntule";
                                break;
                            case "teacher":
                                redirectAction = "ogretmenTabloGoruntule";
                                break;
                            default:
                                TempData["ErrorMessage"] = "Tanımsız kullanıcı rolü!";
                                return RedirectToAction("Giris");
                        }

                        // kullanıcıyı ilgili sayfaya yönlendirme
                        return RedirectToAction(redirectAction, new { id = userID, sistemegiren = retrievedUserName });
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Kullanıcı adı veya şifre yanlış!";
                        return RedirectToAction("Giris");
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Bağlantı sırasında hata oluştu: {ex.Message}";
            }
            finally
            {
                conn.Close();
            }

            return View();
        }


        public void projebilgilerinigonder(Project projebilgileri)
        {
            // oturumdan kullanıcı bilgilerini al
            int userID = (int)Session[SessionUserIDKey];
            string sistemeGiren = (string)Session[SessionUserNameKey];

            
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {              
                    connection.Open();
                    
                    string query = "INSERT INTO projetablo (projeOneriTarihi, projeAdi, projeTuru, projeDanismani, projeYurutucusu, projeEkibi, projeKonusu, projeAmaci, projeHedefKitlesi, projeSuresi, projeButcesi, projeEtkilikleri, projeBaslangicTarihi, projeBitisTarihi, projeKurumKuruluslar, projeMateryaller, sistemeYukleyen, sistemeYukleyenID) " +
                                   "VALUES (@ProjeOneriTarihi, @ProjeAdi, @ProjeTuru, @ProjeDanismani, @ProjeYurutucusu, @ProjeEkibi, @ProjeKonusu, @ProjeAmaci, @ProjeHedefKitlesi, @ProjeSure, @ProjeButcesi, @ProjeEtkilikleri, @ProjeBaslangicTarihi, @ProjeBitisTarihi, @ProjeKurumKuruluslar, @ProjeMateryaller, @SistemeYukleyen, @SistemeYukleyenID)";
                   
                    MySqlCommand command = new MySqlCommand(query, connection);
                    
                    command.Parameters.AddWithValue("@ProjeOneriTarihi", projebilgileri.ProjeOneriTarihi);
                    command.Parameters.AddWithValue("@ProjeAdi", projebilgileri.ProjeAdi);
                    command.Parameters.AddWithValue("@ProjeTuru", projebilgileri.ProjeTuru);
                    command.Parameters.AddWithValue("@ProjeDanismani", projebilgileri.ProjeDanismani);
                    command.Parameters.AddWithValue("@ProjeYurutucusu", projebilgileri.ProjeYurutucusu);
                    command.Parameters.AddWithValue("@ProjeEkibi", projebilgileri.ProjeEkibi);
                    command.Parameters.AddWithValue("@ProjeKonusu", projebilgileri.ProjeKonusu);
                    command.Parameters.AddWithValue("@ProjeAmaci", projebilgileri.ProjeAmaci);
                    command.Parameters.AddWithValue("@ProjeHedefKitlesi", projebilgileri.ProjeHedefKitlesi);
                    command.Parameters.AddWithValue("@ProjeSure", projebilgileri.ProjeSure);
                    command.Parameters.AddWithValue("@ProjeButcesi", projebilgileri.ProjeButcesi);
                    command.Parameters.AddWithValue("@ProjeEtkilikleri", projebilgileri.ProjeEtkilikleri);
                    command.Parameters.AddWithValue("@ProjeBaslangicTarihi", projebilgileri.ProjeBaslangicTarihi);
                    command.Parameters.AddWithValue("@ProjeBitisTarihi", projebilgileri.ProjeBitisTarihi);
                    command.Parameters.AddWithValue("@ProjeKurumKuruluslar", projebilgileri.ProjeKurumKuruluslar);
                    command.Parameters.AddWithValue("@ProjeMateryaller", projebilgileri.ProjeMateryaller);
                    command.Parameters.AddWithValue("@SistemeYukleyen", sistemeGiren);
                    command.Parameters.AddWithValue("@SistemeYukleyenID", userID);

                   
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        TempData["SuccessMessage"] = "İşlem başarıyla kaydedildi.";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "İşlem sırasında bir hata oluştu: Kayıt eklenmedi.";
                    }
                }
                catch (Exception ex)
                {
                    
                    TempData["ErrorMessage"] = "İşlem sırasında bir hata oluştu: " + ex.Message;
                }
            }
        }
        public ActionResult yoneticiSayfasi()
        {
            return View();
        }

        [HttpPost]
        public ActionResult yoneticiSayfasi(Project projebilgileri)
        {

            // projebilgilerinigonder fonksiyonunu çağırarak verileri MySQL veritabanına gönderiyoruz.
            projebilgilerinigonder(projebilgileri);

            return View(projebilgileri);
        }


        public ActionResult ogretmenSayfasi()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ogretmenSayfasi(Project projebilgileri)
        {

          projebilgilerinigonder(projebilgileri);

            return View(projebilgileri);
        }

        public ActionResult ogretmenTabloGoruntule()
        {
            List<object[]> veriListesi = new List<object[]>();

          
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM projetablo";
                MySqlCommand command = new MySqlCommand(query, connection);

                try
                {
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            object[] row = new object[reader.FieldCount];
                            reader.GetValues(row);
                            veriListesi.Add(row);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // hata yönetimi burada ele alınabilir
                }
            }

            return View(veriListesi);
        }

        public ActionResult yoneticiTabloGoruntule()
        {
                List<object[]> veriListesi = new List<object[]>();

              
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = "SELECT * FROM projetablo";
                    MySqlCommand command = new MySqlCommand(query, connection);

                    try
                    {
                        connection.Open();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                object[] row = new object[reader.FieldCount];
                                reader.GetValues(row);
                                veriListesi.Add(row);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        
                    }
                }

                return View(veriListesi);
            }

        // POST: Veri/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
           
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "DELETE FROM projetablo WHERE projeid = @projeid";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@projeid", id);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // hata yönetimi burada ele alınabilir
                }
            }

            // silmeden sonra refresh atıyoruz
            return RedirectToAction("yoneticiTabloGoruntule");
        }

    }
}
