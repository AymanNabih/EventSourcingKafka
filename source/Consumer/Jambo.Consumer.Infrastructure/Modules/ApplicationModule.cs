namespace Jambo.Consumer.Infrastructure.Modules
{
    using Autofac;
    using Jambo.Domain.Model.Blogs;
    using Jambo.Domain.Model.Posts;
    using Jambo.Core.DataAccess.Repositories.Blogs;
    using Jambo.Core.DataAccess.Repositories.Posts;
    using Jambo.Core.DataAccess;

    public class ApplicationModule : Module
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            MongoContext mongoContext = new MongoContext(ConnectionString, DatabaseName);
            mongoContext.DatabaseReset(DatabaseName); //TODO: remove to not clear the DB every time

            builder.Register(c => mongoContext)
                .As<MongoContext>().SingleInstance();

            builder.RegisterType<BlogReadOnlyRepository>()
                .As<IBlogReadOnlyRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<BlogWriteOnlyRepository>()
                .As<IBlogWriteOnlyRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PostReadOnlyRepository>()
                .As<IPostReadOnlyRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PostWriteOnlyRepository>()
                .As<IPostWriteOnlyRepository>()
                .InstancePerLifetimeScope();
        }
    }
}
