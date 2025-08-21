import { Link } from 'react-router'
import { Button } from '@chakra-ui/react'
const Home = () => {
    return (
        <div>
            Welcome Home
            <Link to='/login'>
                <Button colorScheme='blue'>Login</Button>
            </Link>
            <Link to='/signup'>
                <Button colorScheme='teal'>Sign up</Button>
            </Link>
        </div>)
}

export default Home