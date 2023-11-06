﻿namespace ProyectoAPI.Entities
{
    public interface IUtilitarios
    {
        public string ArmarHTML(UsuarioEnt datos, string claveTemporal);

        public string GenerarCodigo();

        public void EnviarCorreo(string Destinatario, string Asunto, string Mensaje);

        public string GenerarToken(string idUsuario);

        public string Encrypt(string texto);

        public string Decrypt(string texto);
    }
}
