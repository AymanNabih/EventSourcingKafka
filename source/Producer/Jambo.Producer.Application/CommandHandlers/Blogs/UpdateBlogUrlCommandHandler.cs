using MediatR;
using System;
using System.Threading.Tasks;
using Jambo.Producer.Application.Commands.Blogs;
using Jambo.Domain.ServiceBus;
using Jambo.Domain.Model.Blogs;
using System.Threading;

namespace Jambo.Producer.Application.CommandHandlers.Blogs
{
    public class UpdateBlogUrlCommandHandler : AsyncRequestHandler<UpdateBlogUrlCommand>
    {
        private readonly IPublisher bus;
        private readonly IBlogReadOnlyRepository blogReadOnlyRepository;

        public UpdateBlogUrlCommandHandler(
            IPublisher bus,
            IBlogReadOnlyRepository blogReadOnlyRepository)
        {
            this.bus = bus ??
                throw new ArgumentNullException(nameof(bus));
            this.blogReadOnlyRepository = blogReadOnlyRepository ??
                throw new ArgumentNullException(nameof(blogReadOnlyRepository));
        }

        protected override async Task Handle(UpdateBlogUrlCommand command, CancellationToken cancellationToken)
        {
            Blog blog = await blogReadOnlyRepository.GetBlog(command.Id);
            blog.UpdateUrl(Url.Create(command.Url));

            await bus.Publish(blog.GetEvents(), command.Header);
        }
    }
}
