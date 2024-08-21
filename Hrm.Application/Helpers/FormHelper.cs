using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hrm.Application.Helpers
{
    public class FormHelper
    {
        private readonly IUnitOfWork _unitOfWork;

        public FormHelper(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CheckDataType(string value, int FieldId)
        {
            var field = await _unitOfWork.Repository<Hrm.Domain.FormField>().Where(x => x.FieldId == FieldId)
               .Include(x => x.FieldType)
               .FirstOrDefaultAsync();

            if (field == null)
            {
                return true;
            }

            var ValidationRegex = field.FieldType.ValidationRegex;

            if (ValidationRegex == null || ValidationRegex =="")
            {
                return true;
            }

            string pattern = $@"{ValidationRegex}";
            Match match = Regex.Match(value, pattern);
            if (match == Match.Empty)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> CheckValidField(int formId, int fieldId)
        {
            bool isValidField = await _unitOfWork.Repository<Hrm.Domain.FormSchema>().Where(x => x.FieldId == fieldId && x.FormId == formId).AnyAsync();

            return isValidField;
        }

        public async Task<bool> CheckMultipleValue(int FormRecordId, int FieldId)
        {
            var formField = await _unitOfWork.Repository<Hrm.Domain.FormField>().Get(FieldId);

            if (formField != null && (formField.HasMultipleValue == null || formField.HasMultipleValue == false))
            {
                var check = await _unitOfWork.Repository<Hrm.Domain.FieldRecord>().Where(x => x.FormRecordId == FormRecordId && x.FieldId == FieldId).AnyAsync();

                if (check)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<bool> IsValidSelectable(string FieldValue, int FieldId)
        {
            var formField = await _unitOfWork.Repository<Hrm.Domain.FormField>().Get(FieldId);
            if (formField != null && formField.HasSelectable != null && formField.HasSelectable == true)
            {
                var check = await _unitOfWork.Repository<Hrm.Domain.SelectableOption>().Where(x => x.OptionValue == FieldValue && x.FieldId == FieldId).AnyAsync();

                return check;
            }

            return true;
        }
    }
}
