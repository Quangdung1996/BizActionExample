using GenericBizRunner.PublicButHidden;
using System;

namespace GenericBizRunner.Configuration.Internal
{
    internal class SetupDtoMappingProfile
    {
        public SetupDtoMappingProfile(Type dtoType, Type bizInOutType, BizRunnerProfile profile, bool bizIn)
        {
            var myGeneric = bizIn ? typeof(SetupToBizDtoProfile<,>) : typeof(SetupFromBizDtoProfile<,>);
            var setupType = myGeneric.MakeGenericType(bizInOutType, dtoType);
            Activator.CreateInstance(setupType, new object[] { profile });
        }

        private class SetupFromBizDtoProfile<TBizOut, TDtoOut>
          where TBizOut : class
          where TDtoOut : GenericActionFromBizDtoSetup<TBizOut, TDtoOut>, new()
        {
            public SetupFromBizDtoProfile(BizRunnerProfile profile)
            {
                dynamic dto = Activator.CreateInstance(typeof(TDtoOut));
                var alterMapExp = dto.AlterDtoMapping;
                if (alterMapExp == null)
                    profile.CreateMap<TBizOut, TDtoOut>().IgnoreAllPropertiesWithAnInaccessibleSetter();
                else
                {
                    alterMapExp(profile.CreateMap<TBizOut, TDtoOut>().IgnoreAllPropertiesWithAnInaccessibleSetter());
                }
            }
        }

        private class SetupToBizDtoProfile<TBizIn, TDtoIn>
            where TBizIn : class
            where TDtoIn : GenericActionToBizDtoSetup<TBizIn, TDtoIn>, new()
        {
            public SetupToBizDtoProfile(BizRunnerProfile profile)
            {
                dynamic dto = Activator.CreateInstance(typeof(TDtoIn));
                var alterMapExp = dto.AlterDtoMapping;
                if (alterMapExp == null)
                    profile.CreateMap<TDtoIn, TBizIn>().IgnoreAllPropertiesWithAnInaccessibleSetter();
                else
                {
                    alterMapExp(profile.CreateMap<TDtoIn, TBizIn>().IgnoreAllPropertiesWithAnInaccessibleSetter());
                }
            }
        }
    }
}