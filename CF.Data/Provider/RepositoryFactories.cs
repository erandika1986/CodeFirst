using CF.Data.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CF.Data
{
    internal class RepositoryFactories
    {
        #region Member Variables

        private readonly IDictionary<Type, Func<DbContext, object>> repositoryFactories;

        #endregion

        #region Constructor

        internal RepositoryFactories()
        {
            this.repositoryFactories = GetCustomFactories();
        }

        #endregion

        #region Private Methods

        private IDictionary<Type, Func<DbContext, object>> GetCustomFactories()
        {
            return new Dictionary<Type, Func<DbContext, object>>
            {
                { typeof(IUserRepository), (dbContext)=> new UserRepository(dbContext)},
                //{ typeof(IPaperTemplateRepository),(dbContext)=>new PaperTemplateRepository(dbContext)},
                //{ typeof(ISectionTemplateRepository),(dbContext)=>new SectionTemplateRepository(dbContext)},
                //{ typeof(IExamRepository),(dbContext)=>new ExamRepository(dbContext)},
                //{ typeof(IPaperRepository),(dbContext)=>new PaperRepository(dbContext)},
                //{ typeof(ISectionRepository),(dbContext)=>new SectionRepository(dbContext)},
                //{ typeof(IQuestionSetRepository),(dbContext)=>new QuestionSetRepository(dbContext)},
                //{ typeof(IQuestionRepository),(dbContext)=>new QuestionRepository(dbContext)},
                //{ typeof(IQuestionTopicRepository),(dbContext)=>new QuestionTopicRepository(dbContext)},
                //{ typeof(IQuestionMainSkillRepository),(dbContext)=>new QuestionMainSkillRepository(dbContext)},
                //{ typeof(IQuestionSubSkillRepository),(dbContext)=>new QuestionSubSkillRepository(dbContext)},
                //{ typeof(IClassRepository),(dbContext)=>new ClassRepository(dbContext)},
                //{ typeof(IUserRoleRepository),(dbContext)=>new UserRoleRepository(dbContext)},
                //{ typeof(IAcademicLevelRepository),(dbContext)=>new AcademicLevelRepository(dbContext)},
                //{ typeof(IAssessmentTypeRepository),(dbContext)=>new AssessmentTypeRepository(dbContext)},
                //{ typeof(IClassNameRepository),(dbContext)=>new ClassNameRepository(dbContext)},
                //{ typeof(ISubjectRepository),(dbContext)=>new SubjectRepository(dbContext)},
                //{ typeof(IExamApprovalRepository),(dbContext)=>new ExamApprovalRepository(dbContext)},
                //{ typeof(IAcademicYearRepository),(dbContext)=>new AcademicYearRepository(dbContext)},
                //{ typeof(ITopicRepository),(dbContext)=>new TopicRepository(dbContext)},
                //{ typeof(IAssessmentTypeAcademicLevelRepository),(dbContext)=>new AssessmentTypeAcademicLevelRepository(dbContext)},
                //{ typeof(IMainSkillRepository),(dbContext)=>new MainSkillRepository(dbContext)},
                //{ typeof(ISubSkillRepository),(dbContext)=>new SubSkillRepository(dbContext)},
                //{ typeof(IStudentRepository),(dbContext)=>new StudentRepository(dbContext)},
                //{ typeof(IStudentClassRepository),(dbContext)=>new StudentClassRepository(dbContext)},
                //{typeof(IStudentPaperResultRepository),(dbContext)=>new StudentPaperResultRepository(dbContext)},
                //{typeof(IStudentQuestionResultRepository),(dbContext)=>new StudentQuestionResultRepository(dbContext)},
                //{typeof(IHeadOfDepartmentRepository),(dbContext)=>new HeadOfDepartmentRepository(dbContext)},
                //{typeof(IClassTeacherRepository),(dbContext)=>new ClassTeacherRepository(dbContext)},
                //{typeof(ISubjectTeacherRepository),(dbContext)=>new SubjectTeacherRepository(dbContext)},
                //{typeof(IExamSetterRepository),(dbContext)=>new ExamSetterRepository(dbContext)},
                //{typeof(ILockingDateRepository),(dbContext)=>new LockingDateRepository(dbContext)},
                //{typeof(IVirtualClassRepository),(dbContext)=>new VirtualClassRepository(dbContext)},
                //{typeof(IVirtualClassStudentRepository),(dbContext)=>new VirtualClassStudentRepository(dbContext)},
                //{typeof(ICoCurricularAttainmentRepository),(dbContext)=>new CoCurricularAttainmentRepository(dbContext)}
            };
        }

        private Func<DbContext, object> DefaultEntityRepositoryFactory<T>() where T : class
        {
            return dbContext => new Repository<T>(dbContext);
        }

        #endregion

        #region Public Methods

        public Func<DbContext, object> GetRepositoryFactory<T>()
        {
            Func<DbContext, object> factory;
            repositoryFactories.TryGetValue(typeof(T), out factory);
            return factory;
        }

        public Func<DbContext, object> GetRepositoryFactoryForEntityType<T>() where T : class
        {
            return GetRepositoryFactory<T>() ?? DefaultEntityRepositoryFactory<T>();
        }

        #endregion
    }
}
