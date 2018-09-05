using MediatR;
using System;
using System.Threading.Tasks;
using Jambo.Producer.Application.Commands.Blogs;
using Jambo.Domain.Model.Blogs;
using Jambo.Domain.ServiceBus;
using System.Threading;

namespace Jambo.Producer.Application.CommandHandlers.Blogs
{
    public class EnableBlogCommandHandler : AsyncRequestHandler<EnableBlogCommand>
    {
        private readonly IPublisher bus;
        private readonly IBlogReadOnlyRepository blogReadOnlyRepository;

        public EnableBlogCommandHandler(
            IPublisher serviceBus,
            IBlogReadOnlyRepository blogReadOnlyRepository)
        {
            bus = serviceBus ??
                throw new ArgumentNullException(nameof(serviceBus));
            this.blogReadOnlyRepository = blogReadOnlyRepository ??
                throw new ArgumentNullException(nameof(blogReadOnlyRepository));
        }

        protected override async Task Handle(EnableBlogCommand request, CancellationToken cancellationToken)
        {
            Blog blog = await blogReadOnlyRepository.GetBlog(request.Id);
            blog.Enable();

            await bus.Publish(blog.GetEvents(), request.Header);
        }
    }
}
