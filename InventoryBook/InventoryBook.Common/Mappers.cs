using InventoryBook.Common.Models;
using InventoryBook.Common.ViewModels;
using System.Collections.Generic;

namespace InventoryBook.Common
{
    public static class Mappers
    {
        public static CategoryViewModel ToCategoryViewModel(this Category category)
        {
            var categoryViewModel = new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name
            };

            return categoryViewModel;
        }

        public static Category ToCategory(this CategoryViewModel categoryViewModel)
        {
            var category = new Category
            {
                Id = categoryViewModel.Id,
                Name = categoryViewModel.Name
            };

            return category;
        }

        public static Item ToItem(this ItemViewModel itemViewModel)
        {
            var item = new Item
            {
                Id = itemViewModel.Id,
                Name = itemViewModel.Name,
                Number = itemViewModel.Number,
                Condition = itemViewModel.Condition,
                Category = itemViewModel.CategoryViewModel.ToCategory(),
                Description = itemViewModel.Description
            };

            return item;
        }

        public static ItemViewModel ToItemViewModel(this Item item)
        {
            var itemViewModel = new ItemViewModel
            {
                Id = item.Id,
                Name = item.Name,
                Number = item.Number,
                Condition = item.Condition,
                CategoryViewModel = item.Category.ToCategoryViewModel(),
                Description = item.Description
            };

            return itemViewModel;
        }

        public static User ToUser(this UserViewModel userViewModel)
        {
            var user = new User
            {
                Id = userViewModel.Id,
                Login = userViewModel.Login,
                Role = userViewModel.Role
            };

            return user;
        }

        public static UserViewModel ToUserViewModel(this User user)
        {
            var userViewModel = new UserViewModel
            {
                Id = user.Id,
                Login = user.Login,
                Role = user.Role
            };

            return userViewModel;
        }

        public static IEnumerable<CategoryViewModel> ToCategoryViewModelList(this IEnumerable<Category> categories)
        {
            var categoryViewModels = new List<CategoryViewModel>();

            foreach (var category in categories)
            {
                categoryViewModels.Add(category.ToCategoryViewModel());
            }

            return categoryViewModels;
        }

        public static IEnumerable<ItemViewModel> ToItemViewModelList(this IEnumerable<Item> items)
        {
            var itemViewModels = new List<ItemViewModel>();

            foreach (var item in items)
            {
                itemViewModels.Add(item.ToItemViewModel());
            }

            return itemViewModels;
        }

        public static IEnumerable<UserViewModel> ToUserViewModelList(this IEnumerable<User> users)
        {
            var userViewModels = new List<UserViewModel>();

            foreach (var user in users)
            {
                userViewModels.Add(user.ToUserViewModel());
            }

            return userViewModels;
        }
    }
}
