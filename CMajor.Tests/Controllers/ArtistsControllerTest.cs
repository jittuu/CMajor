using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using CMajor.Controllers;
using System.Web.Mvc;
using CMajor.Infrastructure;
using Moq;
using CMajor.Models;
using NSupport;
using NFactory;

namespace CMajor.Tests.Controllers {
    public class ArtistsControllerTest : ControllerTest<ArtistsController> {

        public ArtistsControllerTest() {
            this.Controller = new ArtistsController(DbContext);

            InitFactory();
        }

        #region Artists#Index

        [Fact]
        public void Test_artists_index_should_return_ViewResult() {
            var result = Controller.Index();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        [AlwaysCreateDatabase]
        public void Test_artists_index_should_return_with_artists() {
            10.Times(() => Factory.Create<Artist>("artist"));
            var result = Controller.Index();

            var artists = result.Model as IEnumerable<Artist>;
            Assert.Equal(10, artists.Count());

        }

        #endregion

        #region Artists#Create

        [Fact]
        public void Test_artists_create_should_return_ViewResult() {
            var result = Controller.Create();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        [AlwaysCreateDatabase]
        public void Test_artists_create_should_create_new_artist_and_redirect_to_index() {
            var result = Controller.Create(new Artist() { Name = "artist", MyanmarName = "mm_artist" });

            // need to commit since we used UnitOfWorkAttribute in production
            DbContext.Commit();

            var dbArtist = DbContext.Artists.FirstOrDefault(a => a.Name == "artist");

            Assert.NotNull(dbArtist);

            Assert.IsType<RedirectToRouteResult>(result);
            var redirectResult = result as RedirectToRouteResult;
            Assert.Null(redirectResult.RouteValues["controller"] as string);
            Assert.Equal("Index", redirectResult.RouteValues["action"] as string);
        }

        [Fact]
        [AlwaysCreateDatabase]
        public void Test_artists_create_should_return_view_if_modal_is_invalid() {
            Controller.ModelState.AddModelError("*", new Exception());
            var result = Controller.Create(new Artist() { Name = "artist", MyanmarName = "mm_artist" });

            Assert.IsType<ViewResult>(result);
        }

        #endregion

        #region Artists#Edit

        [Fact]
        [AlwaysCreateDatabase]
        public void Test_artists_edit_should_return_ViewResult() {
            var artist = Factory.Create<Artist>("artist");
            var result = Controller.Edit(artist.Id);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        [AlwaysCreateDatabase]
        public void Test_artists_edit_should_return_with_existing_artist() {
            var artist = Factory.Create<Artist>("artist");
            var result = Controller.Edit(artist.Id) as ViewResult;

            var model = result.Model as Artist;
            Assert.Equal(artist.Id, model.Id);
        }

        [Fact]
        [AlwaysCreateDatabase]
        public void Test_artists_edit_should_create_new_artist_and_redirect_to_index() {
            var artist = Factory.Create<Artist>("artist");
            artist.Name = "new name";
            var result = Controller.Edit(artist);

            // need to commit since we used UnitOfWorkAttribute in production
            DbContext.Commit();

            var dbArtist = DbContext.Artists.FirstOrDefault(a => a.Name == "new name");

            Assert.NotNull(dbArtist);

            Assert.IsType<RedirectToRouteResult>(result);
            var redirectResult = result as RedirectToRouteResult;
            Assert.Null(redirectResult.RouteValues["controller"] as string);
            Assert.Equal("Index", redirectResult.RouteValues["action"] as string);
        }

        [Fact]
        [AlwaysCreateDatabase]
        public void Test_artists_edit_should_return_view_if_modal_is_invalid() {
            var artist = Factory.Create<Artist>("artist");
            Controller.ModelState.AddModelError("*", new Exception());
            var result = Controller.Edit(artist);

            Assert.IsType<ViewResult>(result);
        }

        #endregion

        #region#Delete

        [Fact]
        [AlwaysCreateDatabase]
        public void Test_artists_delete_should_return_ViewResult() {
            var artist = Factory.Create<Artist>("artist");
            var result = Controller.Delete(artist.Id);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        [AlwaysCreateDatabase]
        public void Test_artists_delete_should_delete_artist_and_redirect_to_index() {
            var artist = Factory.Create<Artist>("artist");
            var result = Controller.DeleteConfirmed(artist.Id);

            // need to commit since we used UnitOfWorkAttribute in production
            DbContext.Commit();

            var dbArtist = DbContext.Artists.Find(artist.Id);
            Assert.Null(dbArtist);

            Assert.IsType<RedirectToRouteResult>(result);
            var redirectResult = result as RedirectToRouteResult;
            Assert.Null(redirectResult.RouteValues["controller"] as string);
            Assert.Equal("Index", redirectResult.RouteValues["action"] as string);
        }

        #endregion

        private void InitFactory() {
            Factory.Define<Artist>("artist", () => {
                var artist = new Artist() {
                    Name = "Artist",
                    MyanmarName = "mm_artist"
                };
                DbContext.Artists.Add(artist);
                DbContext.SaveChanges();

                return artist;
            });

        }
    }
}