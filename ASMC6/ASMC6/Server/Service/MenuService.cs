using ASMC6.Server.Data;
using ASMC6.Shared;
using System.Collections.Generic;
using System.Linq;

namespace ASMC6.Server.Service
{
    public class MenuService
    {
        private AppDBContext _context;
        public MenuService(AppDBContext context)
        {
            _context = context;
        }
        public List<Menu> GetMenus()
        {
            return _context.Menu.ToList();
        }
        public Menu AddMenu(Menu Menu)
        {
            _context.Add(Menu);
            _context.SaveChanges();
            return Menu;
        }
        public Menu DeleteMenu(int id)
        {
            var existingMenu = _context.Menu.Find(id);
            if (existingMenu == null)
            {
                return null;
            }
            else
            {
                _context.Remove(existingMenu);
                _context.SaveChanges();
                return existingMenu;
            }
        }
        public Menu GetIdMenu(int id)
        {
            var menu = _context.Menu.Find(id);
            if (menu == null)
            {
                return null;
            }
            return menu;
        }
        public Menu UpdateMenu(int id, Menu updateMenu)
        {
            var existingMenu = _context.Menu.Find(id);
            if (existingMenu == null)
            {
                return null;
            }

            existingMenu.RestaurantId = updateMenu.RestaurantId;
            existingMenu.Name = updateMenu.Name;
            existingMenu.IsDelete = updateMenu.IsDelete;


            _context.Update(existingMenu);
            _context.SaveChanges();
            return existingMenu;
        }
    }
}
