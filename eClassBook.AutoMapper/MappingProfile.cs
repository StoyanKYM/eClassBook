using AutoMapper;
using eClassBook.Models;
using eClassBook.Models.SchoolUserEntities;
using eClassBook.Services.DTOs.SchoolUserDTOs;
using eClassBook.ViewModels.Class;
using eClassBook.ViewModels.Parent;
using eClassBook.ViewModels.School;
using eClassBook.ViewModels.Student;
using eClassBook.ViewModels.Subject;
using eClassBook.ViewModels.Teacher;
using eClassBook.ViewModels.User;

namespace eClassBook.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SchoolUser, UserViewModel>()
                .ForMember(o => o.Role,
                    ex => ex.MapFrom(o => o.Role))
                .ForMember(o => o.FullName,
                    ex => ex.MapFrom(o => GetFullName(o)))
                .ForMember(o => o.SchoolUserId,
                    ex => ex.MapFrom(o => o.Id));

            CreateMap<StudentDTO, Student>();
            CreateMap<Student, StudentDTO>()
                .ForMember(o => o.ClassId, ex => ex.MapFrom(o => o.Class.Id));
            CreateMap<StudentEditInputModel, Student>();
            CreateMap<Student, StudentViewModel>()
                .ForMember(o => o.FullName,
                    ex => ex.MapFrom(o => GetFullName(o)))
                .ForMember(o => o.SchoolUserId,
                    ex => ex.MapFrom(o => o.Id))
                .ForMember(o => o.ClassId,
                    ex => ex.MapFrom(o => o.Class.Id));
            CreateMap<Student, StudentTableViewModel>()
                .ForMember(o => o.SchoolUserId,
                    ex => ex.MapFrom(o => o.Id))
                .ForMember(o => o.FullName,
                    ex => ex.MapFrom(o => GetFullName(o)))
                .ForMember(o => o.Grade,
                    ex => ex.MapFrom(o => o.Class.GradeLetter.ToString()
                                          + o.Class.GradeLetter.ToString()))
                .ForMember(o => o.Address,
                    ex => ex.MapFrom(o => o.Town + ", " + o.Address));
            CreateMap<Student, StudentDialogViewModel>()
                .ForMember(o => o.SchoolUserId,
                    ex => ex.MapFrom(o => o.Id))
                .ForMember(o => o.FullName,
                    ex => ex.MapFrom(o => GetFullName(o)))
                .ForMember(o => o.Grade,
                    ex => ex.MapFrom(o => o.Class.GradeLetter.ToString()
                                          + o.Class.GradeLetter.ToString()))
                .ForMember(o => o.Address,
                    ex => ex.MapFrom(o => o.Town + ", " + o.Address))
                .ForMember(o => o.ParentName,
                    ex => ex.MapFrom(o => GetFullName(o.Parent)))
                .ForMember(o => o.Email,
                    ex => ex.MapFrom(o => o.User.Email));
            CreateMap<Student, string>()
                .ConvertUsing(o => GetFullName(o));

            CreateMap<Teacher, TeacherTableViewModel>()
                .ForMember(o => o.SchoolUserId,
                    ex => ex.MapFrom(o => o.Id))
                .ForMember(o => o.FullName,
                    ex => ex.MapFrom(o => GetFullName(o)))
                .ForMember(o => o.Grade,
                    ex => ex.UseDestinationValue())
                .ForMember(o => o.Address,
                    ex => ex.MapFrom(o => o.Town + ", " + o.Address));
            CreateMap<Teacher, TeacherDialogViewModel>()
                .ForMember(o => o.Email,
                    ex => ex.MapFrom(o => o.User.Email))
                .ForMember(o => o.Subjects,
                    ex => ex.MapFrom(o => o.Subjects))
                .ForMember(o => o.AvgScore,
                    ex => ex.UseDestinationValue())
                .ForMember(o => o.SchoolUserId,
                    ex => ex.MapFrom(o => o.Id))
                .ForMember(o => o.FullName,
                    ex => ex.MapFrom(o => GetFullName(o)))
                .ForMember(o => o.Grade,
                    ex => ex.UseDestinationValue())
                .ForMember(o => o.Address,
                    ex => ex.MapFrom(o => o.Town + ", " + o.Address));
            CreateMap<Teacher, MinimalSchoolUserDTO>();


            CreateMap<Parent, ParentViewModel>()
                .ForMember(o => o.SchoolUserId,
                    ex => ex.MapFrom(o => o.Id))
                .ForMember(o => o.FullName,
                    ex => ex.MapFrom(o => GetFullName(o)))
                .ForMember(o => o.Address,
                    ex => ex.MapFrom(o => o.Town + ", " + o.Address))
                .ForMember(o => o.Children,
                    ex => ex.MapFrom(o => o.Children))
                .ForMember(o => o.Email,
                    ex => ex.MapFrom(o => o.User.Email));
            CreateMap<Parent, ParentViewModel>()
                .ForMember(o => o.SchoolUserId,
                    ex => ex.MapFrom(o => o.Id))
                .ForMember(o => o.FullName,
                    ex => ex.MapFrom(o => GetFullName(o)))
                .ForMember(o => o.Address,
                    ex => ex.MapFrom(o => o.Town + ", " + o.Address))
                .ForMember(o => o.Children,
                    ex => ex.MapFrom(o => o.Children))
                .ForMember(o => o.Email,
                    ex => ex.MapFrom(o => o.User.Email))
                .ForMember(o => o.ChildrenData,
                    ex => ex.UseDestinationValue());

            CreateMap<TeacherDTO, Teacher>();
            CreateMap<PrincipalDTO, Principal>();
            CreateMap<ParentDTO, Parent>();
            CreateMap<SchoolAdminDTO, SchoolAdmin>();

            CreateMap<SchoolUser, MinimalSchoolUserDTO>();
                //.Include<Student, MinimalStudentModel>();

            

            CreateMap<Class, ClassViewModel>();
            CreateMap<ClassInputModel, Class>()
                .ForMember(o => o.School, ex => ex.UseDestinationValue());


            CreateMap<Subject, SubjectViewModel>()
                .ForMember(o => o.Grade, ex => ex.MapFrom(o => o.GradeYear))
                .ForMember(o => o.Teachers, ex =>
                    ex.UseDestinationValue());
            CreateMap<SubjectInputModel, Subject>();


            // is this correct?
            CreateMap<Subject, SubjectViewModel>();

            CreateMap<SubjectViewModel, Subject>();
            CreateMap<SubjectViewModel, TeacherToSubject>();
            CreateMap<TeacherToSubject, UserViewModel>();

            // this is correct
            CreateMap<ClassToSubject, SubjectViewModel>();


            // is this correct?
            CreateMap<TeacherToSubject, SubjectViewModel>()
                .ForMember(o => o.Name, ex =>
                    ex.MapFrom(o => o.Subject.Name))
                .ForMember(o => o.Grade, ex =>
                    ex.MapFrom(o => o.Subject.GradeYear))
                .ForMember(o => o.Id, ex =>
                    ex.MapFrom(o => o.SubjectId));
            CreateMap<TeacherToSubject, MinimalSchoolUserDTO>()
                .ForMember(o => o.Id, ex =>
                    ex.MapFrom(o => o.TeacherId))
                .ForMember(o => o.FirstName, ex =>
                    ex.MapFrom(o => o.Teacher.FirstName))
                .ForMember(o => o.SecondName, ex =>
                    ex.MapFrom(o => o.Teacher.SecondName))
                .ForMember(o => o.LastName, ex =>
                    ex.MapFrom(o => o.Teacher.LastName));

            CreateMap<ClassToSubjectInputModel, ClassToSubject>()
                .ForMember(o => o.Teacher, ex =>
                    ex.UseDestinationValue());
            CreateMap<ClassToSubject, ClassToSubjectViewModel>()
                .ForMember(o => o.Grade, ex =>
                    ex.MapFrom(o => o.Class.Grade.ToString() + o.Class.GradeLetter))
                .ForMember(o => o.SubjectName, ex =>
                   ex.MapFrom(o => o.Subject.Name));
            CreateMap<ClassToSubject, ClassToSubjectViewModel>()
                .ForMember(o => o.TeacherName, ex =>
                    ex.MapFrom(o => o.Teacher.FirstName.ToString() + " " +
                                    o.Teacher.SecondName.Substring(0, 1) + ". " +
                                    o.Teacher.LastName))
                .ForMember(o => o.SubjectName, ex =>
                   ex.MapFrom(o => o.Subject.Name));

            CreateMap<ClassToSubject, Subject>()
                .ConvertUsing(o => o.Subject);

            CreateMap<SchoolInputModel, School>();
            CreateMap<School, SchoolViewModel>();
            CreateMap<SchoolViewModel, School>();



            //    /* ------------------- Statistics Mapping ------------------- */
            CreateMap<StudentToGrade, int>().ConvertUsing(o => o.Grade.ValueNum);
            CreateMap<StudentToGrade, double>().ConvertUsing(o => o.Grade.ValueNum);

            CreateMap<Student, Class>().ConvertUsing(o => o.Class);
            CreateMap<ClassToSubject, Subject>().ConvertUsing(o => o.Subject);
            CreateMap<StudentToGrade, Teacher>().ConvertUsing(o => o.Teacher);



            //    /* ------------------- Class Mapping ------------------- */
            CreateMap<Class, ClassTeacherDTO>()
                .ForMember(o => o.TeacherId,
                    ex => ex.MapFrom(o => o.ClassTeacher.Id))
                .ForMember(o => o.Class,
                    ex => ex.MapFrom(o => o.GradeLetter.ToString()
                                          + o.GradeLetter.ToString().ToUpper()));
            CreateMap<Class, string>()
                .ConvertUsing(o => o.Grade.ToString() + o.GradeLetter.ToString().ToUpper());
        }
        private static string GetFullName(SchoolUser user)
        {
            var fullName = user.FirstName;
            if (user.SecondName != null)
            {
                fullName += ' ' + user.SecondName;
            }

            fullName += ' ' + user.LastName;
            return fullName;
        }


    }
      
}