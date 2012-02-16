using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMajor.Controllers;
using Xunit;
using System.Web.Mvc;
using NSupport;
using NFactory;
using CMajor.Models;

namespace CMajor.Tests.Controllers {
    public class SongsControllerTest : ControllerTest<SongsController> {
        public SongsControllerTest() {
            this.Controller = new SongsController(DbContext);

            InitFactory();
        }

        #region Songs#Index

        [Fact]
        public void Test_songs_index_should_return_ViewResult() {
            var result = Controller.Index();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        [AlwaysCreateDatabase]
        public void Test_songs_index_should_return_with_songs() {
            10.Times(() => Factory.Create<Song>("song"));
            var result = Controller.Index();

            var songs = result.Model as IEnumerable<Song>;
            Assert.Equal(10, songs.Count());

        }

        #endregion

        #region Songs#Create

        [Fact]
        public void Test_songs_create_should_return_ViewResult() {
            var result = Controller.Create();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        [AlwaysCreateDatabase]
        public void Test_songs_create_should_create_new_song_and_redirect_to_index() {
            var result = Controller.Create(new Song() { Title = "title", MyanmarTitle = "mm_title", Lyric = "Lyric" });

            // need to commit since we used UnitOfWorkAttribute in production
            DbContext.Commit();

            var dbSong = DbContext.Songs.FirstOrDefault(a => a.Title == "title");

            Assert.NotNull(dbSong);

            Assert.IsType<RedirectToRouteResult>(result);
            var redirectResult = result as RedirectToRouteResult;
            Assert.Null(redirectResult.RouteValues["controller"] as string);
            Assert.Equal("Index", redirectResult.RouteValues["action"] as string);
        }

        [Fact]
        [AlwaysCreateDatabase]
        public void Test_songs_create_should_return_view_if_modal_is_invalid() {
            Controller.ModelState.AddModelError("*", new Exception());
            var result = Controller.Create(new Song() { Title = "song", MyanmarTitle = "mm_song" });

            Assert.IsType<ViewResult>(result);
        }

        #endregion

        #region Songs#Edit

        [Fact]
        [AlwaysCreateDatabase]
        public void Test_songs_edit_should_return_ViewResult() {
            var song = Factory.Create<Song>("song");
            var result = Controller.Edit(song.Id);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        [AlwaysCreateDatabase]
        public void Test_songs_edit_should_return_with_existing_song() {
            var song = Factory.Create<Song>("song");
            var result = Controller.Edit(song.Id) as ViewResult;

            var model = result.Model as Song;
            Assert.Equal(song.Id, model.Id);
        }

        [Fact]
        [AlwaysCreateDatabase]
        public void Test_songs_edit_should_create_new_song_and_redirect_to_index() {
            var song = Factory.Create<Song>("song");
            song.Title = "new name";
            var result = Controller.Edit(song);

            // need to commit since we used UnitOfWorkAttribute in production
            DbContext.Commit();

            var dbSong = DbContext.Songs.FirstOrDefault(a => a.Title == "new name");

            Assert.NotNull(dbSong);

            Assert.IsType<RedirectToRouteResult>(result);
            var redirectResult = result as RedirectToRouteResult;
            Assert.Null(redirectResult.RouteValues["controller"] as string);
            Assert.Equal("Index", redirectResult.RouteValues["action"] as string);
        }

        [Fact]
        [AlwaysCreateDatabase]
        public void Test_songs_edit_should_return_view_if_modal_is_invalid() {
            var song = Factory.Create<Song>("song");
            Controller.ModelState.AddModelError("*", new Exception());
            var result = Controller.Edit(song);

            Assert.IsType<ViewResult>(result);
        }

        #endregion

        #region#Delete

        [Fact]
        [AlwaysCreateDatabase]
        public void Test_songs_delete_should_return_ViewResult() {
            var song = Factory.Create<Song>("song");
            var result = Controller.Delete(song.Id);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        [AlwaysCreateDatabase]
        public void Test_songs_delete_should_delete_song_and_redirect_to_index() {
            var song = Factory.Create<Song>("song");
            var result = Controller.DeleteConfirmed(song.Id);

            // need to commit since we used UnitOfWorkAttribute in production
            DbContext.Commit();

            var dbSong = DbContext.Songs.Find(song.Id);
            Assert.Null(dbSong);

            Assert.IsType<RedirectToRouteResult>(result);
            var redirectResult = result as RedirectToRouteResult;
            Assert.Null(redirectResult.RouteValues["controller"] as string);
            Assert.Equal("Index", redirectResult.RouteValues["action"] as string);
        }

        #endregion

        private void InitFactory() {
            Factory.Define<Song>("song", () => {
                var song = new Song() {
                    Title = "Song",
                    MyanmarTitle = "mm_song",
                    Lyric = "Lyric"
                };
                DbContext.Songs.Add(song);
                DbContext.SaveChanges();

                return song;
            });

        }
    }
}
