using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Ürün Başarıyla Eklendi";
        public static string ProductDeleted = "Ürün Başarıyla Silindi";
        public static string ProductUpdated = "Ürün Başarıyla Güncellendi";

        public static string ProductCategoryDeleted = "Ürün kategorisi ve bağlı olduğu ürünler başarıyla silindi.";
        public static string ProductCategoryAdded = "Ürün kategorisi başarıyla eklendi.";
        public static string ProductCategoryUpdated = "Ürün kategorisi başarıyla güncellendi.";

        public static string UserNotFound = "Kullanıcı Bulunamadı!";
        public static string PasswordError = "Şifre hatalı";
        public static string SuccessfulLogin = "Giriş başarılı";
      
        public static string UserAlreadyExists = "Bu kullanıcı mevcut";
        public static string UserRegistered = "Kullanıcı başarıyla kaydedildi";
        public static string UserAdded = "Kullanıcı başarıyla oluşturuldu";
        public static string UserUpdated = "Kullanıcı başarıyla güncellendi";

        public static string AccessTokenCreated = "Access Token başarıyla oluşturuldu";
        public static string validatorIsNotNull = "Bu alan boş olamaz!!!";
        
    }
}
