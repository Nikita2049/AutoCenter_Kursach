using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace AutoCenterBusinessLogic.ViewModel
{
    [DataContract]
    public class ClientViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [DisplayName("ФИО")]
        public string ClientFIO { get; set; }

        [DataMember]
        [DisplayName("Логин")]
        public string Login { get; set; }

        [DataMember]
        [DisplayName("Пароль")]
        public string Password { get; set; }

        [DataMember]
        [DisplayName("Почта")]
        public string Mail { get; set; }
    }
}