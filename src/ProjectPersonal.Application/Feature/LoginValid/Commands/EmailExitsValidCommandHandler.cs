using BloomFilter;
using Dapper;
using MediatR;
using ProjectPersonal.Application.Common.Interfaces.Repository;
using ProjectPersonal.Application.Common.Interfaces.Result;
using ProjectPersonal.Application.Dtos.Response;
using ProjectPersonal.Domain.Entities;
using ProjectPersonal.Domain.Enum;

namespace ProjectPersonal.Application.Feature.LoginValid.Commands
{
    public class EmailExitsValidCommandHandler : IRequestHandler<EmailExitsValidCommand, Result<EmailEsxitsResponse>>
    {
        private readonly IUnitofwork<User_Partition> _unitOfWork;
        //private static IBloomFilter _bloomFilter;

        public EmailExitsValidCommandHandler(IUnitofwork<User_Partition> unitOfWork/*, IBloomFilter bloomFilter*/)
        {
            _unitOfWork = unitOfWork;
            //_bloomFilter = bloomFilter;
        }

        public async Task<Result<EmailEsxitsResponse>> Handle(EmailExitsValidCommand request, CancellationToken cancellationToken)
        {
            // Kiểm tra nhanh trong BloomFilter
            //if (!_bloomFilter.Contains(request.Email))
            //{
            //    // Nếu không có trong BloomFilter, thêm email vào và trả về kết quả "Không tồn tại"
            //    _bloomFilter.Add(request.Email);
            //    return new Result<EmailEsxitsResponse>
            //    {
            //        Code = (int)Eerrors.Success,
            //        Message = "Not found email exists"
            //    };
            //}

            // Nếu email có thể tồn tại trong BloomFilter, kiểm tra kỹ hơn qua stored procedure
            var pram = new DynamicParameters();
            pram.Add("@email", request.Email, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            pram.Add("@result", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

            await _unitOfWork.GetRepository<User_Partition, Guid>().QueryAsync(
                "FindEmail",
                pram,
                commandType: System.Data.CommandType.StoredProcedure);

            // Lấy kết quả từ stored procedure
            var resultValue = pram.Get<int>("@result");

            // Nếu không tìm thấy email
            if (resultValue == 0)
            {
                //_bloomFilter.Add(request.Email); // Cập nhật BloomFilter để lần sau không cần kiểm tra lại
                return new Result<EmailEsxitsResponse>
                {
                    Code = (int)Eerrors.Notfound,
                    Message = "Not found email exists, Can create account"
                };
            }

            // Nếu email tồn tại
            //_bloomFilter.Add(request.Email); // Đảm bảo BloomFilter luôn được cập nhật
            return new Result<EmailEsxitsResponse>
            {
                Code = (int)Eerrors.EmailIsEsxits,
                Message = "Existed, Can't create account"
            };
        }

    }
}
