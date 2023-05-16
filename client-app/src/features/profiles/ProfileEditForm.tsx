import { Formik} from "formik"
import { observer } from "mobx-react-lite"
import { Form} from "react-router-dom"
import { Button } from "semantic-ui-react"
import * as Yup from 'yup';

import { useStore } from "../../app/stores/store";
import MyTextInput from "../../app/common/form/MyTextInput";
import MyTextArea from "../../app/common/form/MyTextArea";

interface Props {
    setEditMode: (editMode: boolean) => void;
}

export default observer(function ProfileEditForm({setEditMode}: Props) {
    const {profileStore: {profile, updateProfile}} = useStore();

    const validationSchema = Yup.object({
        displayName: Yup.string().required('Display name is required')
    })

    return (
        <Formik 
                initialValues={{displayName: profile?.displayName, bio: profile?.bio}} 
                onSubmit={values => {
                    updateProfile(values).finally(() => {
                        setEditMode(false);
                    })
                }}
                validationSchema={validationSchema}
                >
                    {({ handleSubmit, isValid, isSubmitting, dirty }) => (
                    <Form className='ui form' onSubmit={handleSubmit}>
                        <MyTextInput name='displayName' placeholder='Display Name' />
                        <MyTextArea rows={3} placeholder='Add your bio' name='bio' />
                        <Button 
                            disabled={!dirty || !isValid}
                            loading={isSubmitting} 
                            floated='right' 
                            positive 
                            type='submit' 
                            content='Update Profile' />
                    </Form>
                )}
            </Formik>
    )
})