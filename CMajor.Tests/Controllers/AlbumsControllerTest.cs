using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMajor.Controllers;
using Xunit;
using System.Web.Mvc;
using CMajor.Models;
using NSupport;
using NFactory;

namespace CMajor.Tests.Controllers {
    public class AlbumsControllerTest : ControllerTest<AlbumsController> {
        
        public AlbumsControllerTest() {
            this.Controller = new AlbumsController(DbContext);

            InitFactory();
        }

        #region Albums#Index

        [Fact]
        public void Test_albums_index_should_return_ViewResult() {
            var result = Controller.Index();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        [AlwaysCreateDatabase]
        public void Test_albums_index_should_return_with_albums() {
            10.Times(() => Factory.Create<Album>("album"));
            var result = Controller.Index();

            var albums = result.Model as IEnumerable<Album>;
            Assert.Equal(10, albums.Count());

        }

        #endregion

        #region Albums#Create

        [Fact]
        public void Test_albums_create_should_return_ViewResult() {
            var result = Controller.Create();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        [AlwaysCreateDatabase]
        public void Test_albums_create_should_create_new_album_and_redirect_to_index() {
            var result = Controller.Create(new Album() { Name = "album", MyanmarName = "mm_album" });

            // need to commit since we used UnitOfWorkAttribute in production
            DbContext.Commit();

            var dbAlbum = DbContext.Albums.FirstOrDefault(a => a.Name == "album");

            Assert.NotNull(dbAlbum);

            Assert.IsType<RedirectToRouteResult>(result);
            var redirectResult = result as RedirectToRouteResult;
            Assert.Null(redirectResult.RouteValues["controller"] as string);
            Assert.Equal("Index", redirectResult.RouteValues["action"] as string);
        }

        [Fact]
        [AlwaysCreateDatabase]
        public void Test_albums_create_should_return_view_if_modal_is_invalid() {
            Controller.ModelState.AddModelError("*", new Exception());
            var result = Controller.Create(new Album() { Name = "album", MyanmarName = "mm_album" });

            Assert.IsType<ViewResult>(result);
        }

        #endregion

        #region Albums#Edit

        [Fact]
        [AlwaysCreateDatabase]
        public void Test_albums_edit_should_return_ViewResult() {
            var album = Factory.Create<Album>("album");
            var result = Controller.Edit(album.Id);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        [AlwaysCreateDatabase]
        public void Test_albums_edit_should_return_with_existing_album() {
            var album = Factory.Create<Album>("album");
            var result = Controller.Edit(album.Id) as ViewResult;

            var model = result.Model as Album;
            Assert.Equal(album.Id, model.Id);
        }

        [Fact]
        [AlwaysCreateDatabase]
        public void Test_albums_edit_should_create_new_album_and_redirect_to_index() {
            var album = Factory.Create<Album>("album");
            album.Name = "new name";
            var result = Controller.Edit(album);

            // need to commit since we used UnitOfWorkAttribute in production
            DbContext.Commit();

            var dbAlbum = DbContext.Albums.FirstOrDefault(a => a.Name == "new name");

            Assert.NotNull(dbAlbum);

            Assert.IsType<RedirectToRouteResult>(result);
            var redirectResult = result as RedirectToRouteResult;
            Assert.Null(redirectResult.RouteValues["controller"] as string);
            Assert.Equal("Index", redirectResult.RouteValues["action"] as string);
        }

        [Fact]
        [AlwaysCreateDatabase]
        public void Test_albums_edit_should_return_view_if_modal_is_invalid() {
            var album = Factory.Create<Album>("album");
            Controller.ModelState.AddModelError("*", new Exception());
            var result = Controller.Edit(album);

            Assert.IsType<ViewResult>(result);
        }

        #endregion

        #region Albums#Delete

        [Fact]
        [AlwaysCreateDatabase]
        public void Test_albums_delete_should_return_ViewResult() {
            var album = Factory.Create<Album>("album");
            var result = Controller.Delete(album.Id);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        [AlwaysCreateDatabase]
        public void Test_albums_delete_should_delete_album_and_redirect_to_index() {
            var album = Factory.Create<Album>("album");
            var result = Controller.DeleteConfirmed(album.Id);

            // need to commit since we used UnitOfWorkAttribute in production
            DbContext.Commit();

            var dbAlbum = DbContext.Albums.Find(album.Id);
            Assert.Null(dbAlbum);

            Assert.IsType<RedirectToRouteResult>(result);
            var redirectResult = result as RedirectToRouteResult;
            Assert.Null(redirectResult.RouteValues["controller"] as string);
            Assert.Equal("Index", redirectResult.RouteValues["action"] as string);
        }

        #endregion

        private void InitFactory() {
            Factory.Define<Album>("album", () => {
                var album = new Album() {
                    Name = "Album",
                    MyanmarName = "mm_album"
                };
                DbContext.Albums.Add(album);
                DbContext.SaveChanges();

                return album;
            });

        }
    }
}
