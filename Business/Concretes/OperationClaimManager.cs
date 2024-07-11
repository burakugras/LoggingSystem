using AutoMapper;
using Business.Abstracts;
using Business.Dtos.Requests.ActivityRequests;
using Business.Dtos.Requests.OperationClaimRequests;
using Business.Dtos.Responses.ActivityResponses;
using Business.Dtos.Responses.OperationClaimResponses;
using Core.DataAccess.Paging;
using Core.Entities.Concretes;
using DataAccess.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class OperationClaimManager : IOperationClaimService
    {
        IOperationClaimDal _operationClaimDal;
        IMapper _mapper;

        public OperationClaimManager(IOperationClaimDal operationClaimDal, IMapper mapper)
        {
            _mapper = mapper;
            _operationClaimDal = operationClaimDal;            
        }

        public async Task<CreatedOperationClaimResponse> AddAsync(CreateOperationClaimRequest createOperationClaimRequest)
        {
            OperationClaim operationClaim = _mapper.Map<OperationClaim>(createOperationClaimRequest);
            OperationClaim createdOperationClaim = await _operationClaimDal.AddAsync(operationClaim);
            CreatedOperationClaimResponse createdOperationClaimResponse = _mapper.Map<CreatedOperationClaimResponse>(createdOperationClaim);
            return createdOperationClaimResponse;

        }

        public async Task<DeletedOperationClaimResponse> DeleteAsync(int id)
        {
            OperationClaim operationClaim = await _operationClaimDal.GetAsync(predicate: op => op.Id == id);
            OperationClaim deletedOperationClaim = await _operationClaimDal.DeleteAsync(operationClaim);
            DeletedOperationClaimResponse deletedOperationClaimResponse = _mapper.Map<DeletedOperationClaimResponse>(deletedOperationClaim);
            return deletedOperationClaimResponse;

        }

        public async Task<IPaginate<GetListOperationClaimResponse>> GetListAsync(PageRequest pageRequest)
        {
            var operationClaim = await _operationClaimDal.GetListAsync(
                                   index: pageRequest.PageIndex,
                                   size: pageRequest.PageSize
                                   );
            var mappedOperationClaim = _mapper.Map<Paginate<GetListOperationClaimResponse>>(operationClaim);
            return mappedOperationClaim;

        }

        public async Task<UpdatedOperationClaimResponse> UpdateAsync(UpdateOperationClaimRequest updateOperationClaimRequest)
        {
            OperationClaim operationClaim = _mapper.Map<OperationClaim>(updateOperationClaimRequest);
            OperationClaim updatedOperationClaim = await _operationClaimDal.UpdateAsync(operationClaim);
            UpdatedOperationClaimResponse updatedOperationClaimResponse = _mapper.Map<UpdatedOperationClaimResponse>(updatedOperationClaim);
            return updatedOperationClaimResponse;

        }
    }
}
