using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMajor.Controllers;
using CMajor.Models;

namespace CMajor.Tests {
    public class ControllerTest<T> where T : ApplicationController {
        public ControllerTest() {
            this.DbContext = new CMajorDbContext();
        }

        public CMajorDbContext DbContext { get; private set; }

        protected T Controller { get; set; }
    }
}
