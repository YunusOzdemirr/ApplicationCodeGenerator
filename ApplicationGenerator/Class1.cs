using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationGenerator
{
    public class Class1
    {

     public static void CreateHandlerCreated()
        {
            List<string> entities = new List<string>() {"Basket" ,
            "BasketItem" ,
            "Category" ,
            "OperationClaim" ,
            "Option" ,
            "Order" ,
            "Product" ,
            "ProductAndCategory" ,
            "ProductPicture" ,
            "Report" ,
            "User" ,
            "UserAndOperationClaim" ,
            "UserToken"};
            foreach (var item in entities)
            {
                string entityName = item;
                //string projectDirectory = @"C:\Users\yusuf\OneDrive\Belgeler\GitHub\beryque\Business\Beryque.Application\Features\Commands";
                string projectDirectory = @"C:\\Users\\yunus\\Documents\\GitHub\\beryque\\Business\\Beryque.Application\Features\Commands\";
                string targetDirectory = Path.Combine(projectDirectory, $"{entityName}Commands");
                string Crud = "Get";
                string fileName = $"{Crud}{entityName}CommandHandler.cs";


                if (!Directory.Exists(targetDirectory))
                {
                    Directory.CreateDirectory(targetDirectory);
                }

                string generatedCode = $@"
                using Beryque.Domain.Entities;
                using MediatR;

                namespace Beryque.Application.Features.Commands.{entityName}Commands
                {{
                    public class {Crud}{entityName}CommandHandler : IRequestHandler<{Crud}{entityName}Command, Unit>
                    {{
                        readonly private I{entityName}Repository _{entityName.ToLower()}Repository;
                        readonly private IMapper _mapper;

                        public {Crud}{entityName}CommandHandler(I{entityName}Repository {entityName.ToLower()}Repository, IMapper mapper)
                        {{
                            _{entityName.ToLower()}Repository = {entityName.ToLower()}Repository;
                            _mapper = mapper;
                        }}

                        public async Task<Unit> Handle({Crud}{entityName}Command request, CancellationToken cancellationToken)
                        {{
                            var {entityName.ToLower()} = _mapper.Map<{entityName}>(request);
                            await _{entityName.ToLower()}Repository.GetByIdAsync({entityName.ToLower()}.Id);
                            return Unit.Value;
                        }}
                    }}
                }}
            ";

                string filePath = Path.Combine(targetDirectory, fileName);
                File.WriteAllText(filePath, generatedCode);


            }

        }
    }

}
