using HeresyCore.Descriptions;
using HeresyCore.Entities;
using HeresyCore.Utilities;
using ServiceTester.HeresyAuthService;
using ServiceTester.HeresyService;
using ServiceTester.Utilities.Interfaces;
using System;
using System.Collections.Generic;

namespace ServiceTester.Utilities
{
    public class AuthorizedContext : WcfContext<AuthServiceClient>, IDisposable
    {
        internal HeresyServiceClient Main;

        public Token Token { get; private set; }

        private IDictionary<string, CharacterContext> _characterList;

        public IDictionary<string, CharacterContext> CharacterList
        {
            get
            {
                if (_characterList == null)
                {
                    _characterList = GetCharacterContextList();
                }
                return _characterList;
            }
        }

        public static void Using(Action<HeresyServiceClient, AuthorizedContext> worker) => WcfExtensions.Using<HeresyServiceClient>((service, mainContext) =>
        {
            using (var auth = new AuthorizedContext(mainContext))
            {
                worker(service, auth);
            }
        });

        #region Constructors

        public AuthorizedContext(WcfContext<HeresyServiceClient> heresyServiceContext) : base(new AuthServiceClient())
        {
            Main = heresyServiceContext.Service;
        }

        public AuthorizedContext(WcfContext<HeresyServiceClient> heresyServiceContext, string login, string password) : this(heresyServiceContext)
        {
            Login(login, password);
        }

        public AuthorizedContext(WcfContext<HeresyServiceClient> heresyServiceContext, Token token) : this(heresyServiceContext)
        {
            Token = token;
        }

        #endregion

        #region Methods

        #region Auth

        public bool Login(string login, string password)
        {
            if (Token != null && !Logout())
                return false;

            var token = Service.Login(login, password);

            if (token == null)
                return false;

            Token = token;
            return true;
        }

        public bool Logout()
        {
            if (Token == null)
                throw new Exception("Пользователь не в сети");

            if (Service.Logout(Token))
            {
                Token = null;
                return true;
            }

            return false;
        }

        public bool IsLogged() => Token != null && Service.IsLogged(Token);

        public bool Register(string login, string password) => Service.Register(login, password);

        #endregion

        #region Main

        private TResult Authorized<TResult>(Func<Token, TResult> worker)
        {
            if (Token == null)
                throw new Exception("Пользователь не в сети");

            return worker(Token);
        }

        private CharacterContext CharacterGetter(Func<CharacterDescription> getter)
        {
            var c = getter();

            if (c != null)
            {
                var charContext = new CharacterContext(this, c);
                _characterList[c.Id] = charContext;
                return charContext;
            }

            return null;
        }

        public IDictionary<string, CharacterContext> GetCharacterContextList() => Authorized(token =>
            _characterList = Main.GetCharacterDescriptionList(Token)
                .Remap(c => new CharacterContext(this, c))
        );

        public IDictionary<string, ICharacterManagementContext> GetCharacterList() => Authorized(token =>
            GetCharacterContextList()
                .Remap<string, CharacterContext, ICharacterManagementContext>(c => c)
        );

        public ICharacterCreationContext CreateCharacter(string name) => Authorized(token => CharacterGetter(() =>
        {
            var cid = Main.CreateCharacter(Token, name);
            if (cid == null)
                return null;
            
            return Main.GetCharacterDescription(Token, cid);
        }));

        public CharacterContext GetCharacterContext(string id) => Authorized(token => CharacterGetter(() =>
            Main.GetCharacterDescription(Token, id)
        ));

        public ICharacterManagementContext GetCharacter(string id) =>
            GetCharacterContext(id);

        #endregion

        #endregion

        #region IDisposable

        private bool _isDisposed = false;

        public override void Dispose()
        {
            if (_isDisposed)
                return;
            
            base.Dispose();
            Main = null;

            _isDisposed = true;
        }

        #endregion
    }
}
