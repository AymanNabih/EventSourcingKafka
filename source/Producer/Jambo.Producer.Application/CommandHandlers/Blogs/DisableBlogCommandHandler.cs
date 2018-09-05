using Jambo.Producer.Application.Commands.Blogs;
using Jambo.Domain.Model.Blogs;
using MediatR;
using System;
using System.Threading.Tasks;
using Jambo.Domain.ServiceBus;
using System.Threading;

namespace Jambo.Producer.Application.CommandHandlers.Blogs
{
    public class DisableBlogCommandHandler : AsyncRequestHandler<DisableBlogCommand>
    {
        private readonly IPublisher bus;
        private readonly IBlogReadOnlyRepository blogReadOnlyRepository;

        public DisableBlogCommandHandler(
            IPublisher bus,
            IBlogReadOnlyRepository blogReadOnlyRepository)
        {
            this.bus = bus ?? throw new ArgumentNullException(nameof(bus));
            this.blogReadOnlyRepository = blogReadOnlyRepository ??
                throw new ArgumentNullException(nameof(blogReadOnlyRepository));
        }

        protected override async Task Handle(DisableBlogCommand command, CancellationToken cancellationToken)
        {
            Blog blog = await blogReadOnlyRepository.GetBlog(command.Id);
            blog.Disable();

            await bus.Publish(blog.GetEvents(), command.Header);
        }

    }
}
