
using Firebase.Auth;
using Firebase.Storage;

namespace ProyectoLibreria.Services
{
    public class ServicioImagen : IServicioImagen
    {
        public async Task<string> SubirImagen(Stream archivo, string nombre)
        {
            string email = "kchala900@gmail.com";
            string clave = "chala123";
            string ruta = "proyectolibreria-36494.appspot.com";
            string api_key = "AIzaSyD7c03CVTmsx_OIhhCrbroOiY1sHmW2Rc0";

            var auth = new FirebaseAuthProvider(new FirebaseConfig(api_key));
            var a = await auth.SignInWithEmailAndPasswordAsync(email, clave);

            var cancellation = new CancellationTokenSource();

            var task = new FirebaseStorage(
              ruta,
              new FirebaseStorageOptions
              {
                  AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                  ThrowOnCancel = true
              })

               .Child("Fotos_Perfil")
               .Child(nombre)
               .PutAsync(archivo, cancellation.Token);

            var downloadURL = await task;
            return downloadURL;
        }
    }
}
