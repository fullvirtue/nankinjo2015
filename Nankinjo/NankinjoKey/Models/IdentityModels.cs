using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Linq;
using System.Web;

namespace NankinjoKey.Models
{
    // ApplicationUser クラスにプロパティを追加することでユーザーのプロファイル データを追加できます。詳細については、http://go.microsoft.com/fwlink/?LinkID=317594 を参照してください。
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // authenticationType が CookieAuthenticationOptions.AuthenticationType で定義されているものと一致している必要があります
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // ここにカスタム ユーザー クレームを追加します
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //=====================================================
        //レコード登録日・更新日を入れる
        //=====================================================
        public override int SaveChanges()
        {
            var now = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now.ToUniversalTime(), "Tokyo Standard Time");
            SetCreateDateTime(now);
            SetUpdateDateTime(now);
            return base.SaveChanges();
            //try
            //{
            //    return base.SaveChanges();
            //}
            //catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            //{
            //    foreach (var errors in ex.EntityValidationErrors)
            //    {
            //        foreach (var error in errors.ValidationErrors)
            //        {
            //            System.Diagnostics.Trace.WriteLine(error.ErrorMessage);    // VisualStudioの出力に表示
            //        }
            //    }
            //    throw ex;
            //}

        }
        private void SetUpdateDateTime(DateTime now)
        {
            var entities = this.ChangeTracker.Entries()
                .Where(e => (e.State == EntityState.Added || e.State == EntityState.Modified) && (e.CurrentValues.PropertyNames.Contains("UpdateDateTime") || e.CurrentValues.PropertyNames.Contains("UpdateUser")))
                .Select(e => e.Entity);

            foreach (dynamic entity in entities)
            {

                entity.UpdateDateTime = now;
                entity.UpdateUser = HttpContext.Current.User.Identity.Name;
            }
        }
        private void SetCreateDateTime(DateTime now)
        {
            var entities = this.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added && (e.CurrentValues.PropertyNames.Contains("CreateDateTime") || e.CurrentValues.PropertyNames.Contains("CreateUser")))
                .Select(e => e.Entity);

            foreach (dynamic entity in entities)
            {
                entity.CreateDateTime = now;
                entity.CreateUser = HttpContext.Current.User.Identity.Name;
            }
        }

        public System.Data.Entity.DbSet<NankinjoKey.Models.KeyInfo> KeyInfoes { get; set; }

        public System.Data.Entity.DbSet<NankinjoKey.Models.KeyLog> KeyLogs { get; set; }
    }
}