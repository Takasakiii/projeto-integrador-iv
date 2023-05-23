import { userApi } from "@/core/api/user";
import { UserType } from "@/core/models/user";
import { yupResolver } from "@hookform/resolvers/yup";
import { useForm } from "react-hook-form";
import { useRouter } from "next/navigation";
import * as yup from "yup";

const schema = yup.object({
  name: yup.string().required(),
  email: yup.string().required().email(),
  password: yup.string().required().min(8),
  repeatPassword: yup.string().required().min(8),
  isCompany: yup.bool().required(),
});

export type RegisterForm = yup.InferType<typeof schema>;

export const useFormRegister = () => {
  const {
    register,
    handleSubmit,
    formState: { isSubmitting, errors },
  } = useForm<RegisterForm>({
    resolver: yupResolver(schema),
  });
  const router = useRouter();

  const onSubmit = handleSubmit(async (values) => {
    const user = userApi.post({
      ...values,
      type: values.isCompany ? UserType.Company : UserType.Professional,
    });
    router.push("/auth/login");
  });

  return {
    register,
    isSubmitting,
    onSubmit,
    errors,
  };
};
