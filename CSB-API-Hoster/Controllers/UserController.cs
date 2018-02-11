using CSB_API_Hoster;
using CSB_API_Hoster.Controllers.UserResourses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace APIHoster.Controller
{
    public class UserController : ApiController
    {
        UserRepository userRepository;

        public UserController()
        {
            userRepository = new UserRepository();
        }

        // GET api/user
        [HttpGet]
        public IHttpActionResult GetAllUsers()
        {
            Console.WriteLine("{0} -> Get() Method Called.", DateTime.Now.ToString(@"h\:mm:ss tt"));

            UserResponse response = new UserResponse();

            try
            {
                response.UserVM = userRepository.GetAllUsers();

                Console.WriteLine("{0} -> {1} User(s), returned.", DateTime.Now.ToString(@"h\:mm:ss tt"), response.UserVM.Count());

                response.IsSuccess = true;
                response.Status = 200;
            }
            catch (Exception ex)
            {
                response.UserVM = null;

                Console.WriteLine("{0} -> Exception occured - {1}", DateTime.Now.ToString(@"h\:mm:ss tt"), ex.Message);

                response.IsSuccess = false;
                response.Status = 500;
            }

            Console.WriteLine();

            return Content(HttpStatusCode.OK, response);
        }

        // GET api/user/5
        public IHttpActionResult Get(int id)
        {
            Console.WriteLine("{0} -> Get(id = {1}) Method Called.", DateTime.Now.ToString(@"h\:mm:ss tt"), id);

            UserResponse response = new UserResponse();

            try
            {
                UserVM user = userRepository.GetUserById(id);
                response.UserVM = new List<UserVM>() { user };

                if (user != null)
                {
                    Console.WriteLine("{0} -> User found. 1 User, returned.", DateTime.Now.ToString(@"h\:mm:ss tt"));
                    response.Status = 200;
                }
                else
                {
                    Console.WriteLine("{0} -> User not found. NULL, returned.", DateTime.Now.ToString(@"h\:mm:ss tt"));
                    response.Status = 404;
                }
                
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.UserVM = null;

                Console.WriteLine("{0} -> Exception occured - {1}", DateTime.Now.ToString(@"h\:mm:ss tt"), ex.Message);

                response.IsSuccess = false;
                response.Status = 500;
            }

            Console.WriteLine();

            return Content(HttpStatusCode.OK, response);
        }

        // POST api/user
        public IHttpActionResult Post([FromBody]UserRequest request)
        {
            Console.WriteLine("{0} -> Post() Method Called.", DateTime.Now.ToString(@"h\:mm:ss tt"));

            UserResponse response = new UserResponse();

            try
            {
                UserVM user = userRepository.AddNewUser(request.UserVM);
                response.UserVM = new List<UserVM>() { user };

                if (user != null)
                {
                    Console.WriteLine("{0} -> User added to database.", DateTime.Now.ToString(@"h\:mm:ss tt"));

                    response.IsSuccess = true;
                    response.Status = 201;
                }
                else
                {
                    Console.WriteLine("{0} -> User adding to database failed.", DateTime.Now.ToString(@"h\:mm:ss tt"));

                    response.IsSuccess = false;
                    response.Status = 500;
                }
            }
            catch (Exception ex)
            {
                response.UserVM = null;

                Console.WriteLine("{0} -> Exception occured - {1}", DateTime.Now.ToString(@"h\:mm:ss tt"), ex.Message);

                response.IsSuccess = false;
                response.Status = 500;
            }

            Console.WriteLine();

            return Content(HttpStatusCode.OK, response);
        }

        // PUT api/user
        public IHttpActionResult Put([FromBody]UserRequest request)
        {
            UserResponse response = new UserResponse();

            try
            {
                Console.WriteLine("{0} -> Put(id = {1}) Method Called.", DateTime.Now.ToString(@"h\:mm:ss tt"), request.UserVM.Id);

                UserVM user = userRepository.UpdateUser(request.UserVM);
                response.UserVM = new List<UserVM>() { user };

                if (user != null)
                {
                    Console.WriteLine("{0} -> User data updated.", DateTime.Now.ToString(@"h\:mm:ss tt"));

                    response.IsSuccess = true;
                    response.Status = 200;
                }
                else
                {
                    response.UserVM = null;

                    Console.WriteLine("{0} -> User not found. Data discarded.", DateTime.Now.ToString(@"h\:mm:ss tt"));

                    response.IsSuccess = false;
                    response.Status = 404;
                }
            }
            catch (Exception ex)
            {
                response.UserVM = null;

                Console.WriteLine("{0} -> Exception occured - {1}", DateTime.Now.ToString(@"h\:mm:ss tt"), ex.Message);

                response.IsSuccess = false;
                response.Status = 500;
            }

            Console.WriteLine();

            return Content(HttpStatusCode.OK, response);
        }

        // DELETE api/demo/5
        public IHttpActionResult Delete(int id)
        {
            Console.WriteLine("{0} -> Delete(id = {1}) Method Called.", DateTime.Now.ToString(@"h\:mm:ss tt"), id);

            UserResponse response = new UserResponse();
            response.UserVM = null;

            try
            {
                response.IsSuccess = userRepository.DeleteUser(id);

                if (response.IsSuccess)
                {
                    Console.WriteLine("{0} -> User deleted from database.", DateTime.Now.ToString(@"h\:mm:ss tt"));
                    response.Status = 200;
                }
                else
                {
                    Console.WriteLine("{0} -> User not found. User not deleted.", DateTime.Now.ToString(@"h\:mm:ss tt"));
                    response.Status = 404;
                }
            }
            catch (Exception ex)
            {
                response.UserVM = null;

                Console.WriteLine("{0} -> Exception occured - {1}", DateTime.Now.ToString(@"h\:mm:ss tt"), ex.Message);

                response.IsSuccess = false;
                response.Status = 500;
            }

            Console.WriteLine();

            return Content(HttpStatusCode.OK, response);
        }
    }
}
